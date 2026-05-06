using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizManagment.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Register Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string name, string email, string username, string password)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "All fields are required.";
                return View("Index");
            }

            if (password.Length < 6)
            {
                ViewBag.ErrorMessage = "Password must be at least 6 characters.";
                return View("Index");
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString")
                ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

            try
            {
                // Hash the password before storing
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand("PR_User_Register", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = username;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = hashedPassword;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                command.Parameters.Add("@Mobile", SqlDbType.VarChar, 100).Value = ""; // Optional field
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = true;
                command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = false;

                connection.Open();
                command.ExecuteNonQuery();

                return RedirectToAction("Success");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Unique constraint violation
                ViewBag.ErrorMessage = "Username or email already exists.";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Registration failed: " + ex.Message;
                return View("Index");
            }
        }
        #endregion

        #region Success Page
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        #endregion
    }
}
