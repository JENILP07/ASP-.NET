using Microsoft.AspNetCore.Mvc;
using QuizManagment.Filters;
using QuizManagment.Models;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace QuizManagment.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        /*rivate readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
        #region Configuration
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public IActionResult Index()
        {
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_Quiz_SelectAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            table.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading Quizes : " + ex.Message;
                return View(new DataTable()); // Ensure an empty DataTable is returned
            }

            return View(table.Rows.Count > 0 ? table : new DataTable());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
