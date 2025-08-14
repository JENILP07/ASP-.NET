using Microsoft.AspNetCore.Mvc;
using QuizManagment.Models;
using System.Data.SqlClient;
using System.Data;

namespace QuizManagment.Controllers
{
    public class LoginUserController : Controller
    {
        private IConfiguration Configuration;

        public LoginUserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            string ErrorMsg = string.Empty;

            if (string.IsNullOrEmpty(userLoginModel.UserName))
            {
                ErrorMsg += "User Name is Required";
            }

            if (string.IsNullOrEmpty(userLoginModel.Password))
            {
                ErrorMsg += "<br/>Password is Required";
            }

            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("ConnectionString"));
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();

                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.CommandText = "PR_User_Login";
                objCmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                objCmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                SqlDataReader objSDR = objCmd.ExecuteReader();

                DataTable dtLogin = new DataTable();

                // Check if Data Reader has Rows or not
                if (!objSDR.HasRows)
                {
                    TempData["ErrorMessage"] = "Invalid User Name or Password";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    dtLogin.Load(objSDR);

                    // Load the retrieved data to session through HttpContext.
                    foreach (DataRow dr in dtLogin.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("MobileNo", dr["Mobile"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = ErrorMsg;
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the session to log out the user
            HttpContext.Session.Clear();

            // Redirect to the login page (usually the Index action of LoginUserController)
            return RedirectToAction("Index", "Login");
        }

    }

}