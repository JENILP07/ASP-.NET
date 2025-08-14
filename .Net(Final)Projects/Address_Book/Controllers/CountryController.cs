using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Address_Book.Models;

namespace Address_Book.Controllers
{
    public class CountryController : Controller
    {
        private IConfiguration configuration;

        public CountryController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult CountryList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_GetAllCountries";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult CountryDelete(int CountryID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SP_DeleteCountry";
                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Country deleted successfully.";
                return RedirectToAction("CountryList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Country: " + ex.Message;
                return RedirectToAction("CountryList");
            }
        }
        public IActionResult AddCountry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CountryAdd(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;


                command.CommandText = "SP_InsertCountry";
                command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = model.CountryName;
                command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = model.CountryCode;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("CountryList");
            }

            return View("AddCountry", model);
        }
      

    }
}
