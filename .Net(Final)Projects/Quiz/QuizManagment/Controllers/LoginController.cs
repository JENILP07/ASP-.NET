using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;

namespace QuizManagment.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Login Page
        [HttpGet]
        public IActionResult Index()
        {
            // If already logged in, redirect to Home
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion

        #region Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View("Index");
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString")
                ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand("PR_User_Login", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = username;

                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                if (dt.Rows.Count == 0)
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index");
                }

                DataRow user = dt.Rows[0];
                string storedPassword = user["Password"].ToString() ?? "";

                // Verify hashed password
                bool isValidPassword;
                try
                {
                    // Try BCrypt verification first (for hashed passwords)
                    isValidPassword = BCrypt.Net.BCrypt.Verify(password, storedPassword);
                }
                catch
                {
                    // Fallback: plain-text comparison for legacy passwords
                    isValidPassword = (password == storedPassword);

                    // If legacy password matches, upgrade it to BCrypt hash
                    if (isValidPassword)
                    {
                        UpgradePasswordToHash(Convert.ToInt32(user["UserID"]), password, connectionString);
                    }
                }

                if (!isValidPassword)
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index");
                }

                // Check if user is active
                if (!Convert.ToBoolean(user["IsActive"]))
                {
                    ViewBag.ErrorMessage = "Your account is deactivated. Contact administrator.";
                    return View("Index");
                }

                // Create authentication claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user["UserName"].ToString() ?? ""),
                    new Claim("UserID", user["UserID"].ToString() ?? "0"),
                    new Claim(ClaimTypes.Email, user["Email"].ToString() ?? ""),
                    new Claim(ClaimTypes.Role, Convert.ToBoolean(user["IsAdmin"]) ? "Admin" : "User")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Login failed: " + ex.Message;
                return View("Index");
            }
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Upgrade Legacy Password
        /// <summary>
        /// Silently upgrades a plain-text password to BCrypt hash when a user logs in with a legacy password.
        /// </summary>
        private void UpgradePasswordToHash(int userId, string plainPassword, string connectionString)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand(
                    "UPDATE MST_User SET Password = @Password, Modified = @Modified WHERE UserID = @UserID",
                    connection);

                command.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = hashedPassword;
                command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
                // Silently fail — password upgrade is best-effort
            }
        }
        #endregion
    }
}
