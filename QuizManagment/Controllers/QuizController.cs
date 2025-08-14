using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using QuizManagment.Models;
using OfficeOpenXml;
using QuizManagment.Filters;

namespace QuizManagment.Controllers
{
    [CheckAccess]
    public class QuizController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;

        public QuizController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Index
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
        #endregion

        #region Delete
        public IActionResult Delete(int QuizID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_Quiz_DeleteByPK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@QuizID", QuizID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting quiz: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add / Edit Quiz 
        // Action to add or edit a quiz
        public IActionResult QuizAddEdit(Quiz model)
        {
            if (!ModelState.IsValid)
            {
                UserDropDown(); // Ensure dropdown is repopulated in case of validation failure
                return View("Form", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    if (model.QuizID == 0) // New quiz, so insert
                    {
                        command.CommandText = "PR_Quiz_Insert";
                        TempData["SuccessMessage"] = "Quiz Inserted successfully!";
                    }
                    else // Existing quiz, so update
                    {
                        command.CommandText = "PR_Quiz_UpdateByPK";
                        command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                        command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                        TempData["SuccessMessage"] = "Quiz U    pdated successfully!";
                    }

                    // Add the quiz parameters
                    command.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = model.QuizName;
                    command.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                    command.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;

                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving quiz: " + ex.Message;
                UserDropDown(); // Repopulate dropdown before returning
                return View("Form", model);
            }
        }
        #endregion

        #region AddForm
        // Display the form for adding or editing a quiz
        public IActionResult Form(int? QuizID)
        {
            UserDropDown(); // Populate user dropdown

            if (QuizID == null)
            {
                var model = new Quiz { Created = DateTime.Now }; // New quiz object
                return View("Form", model); // Return form with empty model
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_Quiz_SelectByPK", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@QuizID", QuizID);
                    using (var reader = command.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);

                        var model = new Quiz();
                        foreach (DataRow dataRow in table.Rows)
                        {
                            model.QuizID = Convert.ToInt32(dataRow["QuizID"]);
                            model.QuizName = dataRow["QuizName"].ToString();
                            model.TotalQuestions = Convert.ToInt32(dataRow["TotalQuestions"]);
                            model.QuizDate = Convert.ToDateTime(dataRow["QuizDate"]);
                            model.UserID = Convert.ToInt32(dataRow["UserID"]);
                        }

                        return View("Form", model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading quiz details: " + ex.Message;
                return View("Form", new Quiz());
            }
        }
        #endregion

        #region User DropDown
        // Populate the user dropdown list
        private void UserDropDown()
        {
            ViewBag.UserList = new List<UserDropDown>();

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_USER_DROPDOWN", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        var userList = new List<UserDropDown>();
                        foreach (DataRow data in dataTable.Rows)
                        {
                            userList.Add(new UserDropDown
                            {
                                UserID = Convert.ToInt32(data["UserID"]),
                                UserName = data["UserName"].ToString()
                            });
                        }

                        ViewBag.UserList = userList;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading users: " + ex.Message;
            }
        }
        #endregion

        #region Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Quiz_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "Quiz ID";
                worksheet.Cells[1, 2].Value = "Quiz Name";
                worksheet.Cells[1, 3].Value = "Total Question";
                worksheet.Cells[1, 4].Value = "Quiz Date";
                worksheet.Cells[1, 5].Value = "User Name";
                worksheet.Cells[1, 6].Value = "Created";
                worksheet.Cells[1, 7].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuizID"];
                    worksheet.Cells[row, 2].Value = item["QuizName"];
                    worksheet.Cells[row, 3].Value = item["TotalQuestions"];

                    // Ensure QuizDate is correctly formatted as DateTime in Excel
                    if (item["QuizDate"] != DBNull.Value)
                    {
                        DateTime quizDate = Convert.ToDateTime(item["QuizDate"]);
                        worksheet.Cells[row, 4].Value = quizDate;
                        worksheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Optional: Format date in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 4].Value = "N/A";
                    }

                    // Ensure Created field is correctly formatted as DateTime in Excel
                    if (item["Created"] != DBNull.Value)
                    {
                        DateTime createdDate = Convert.ToDateTime(item["Created"]);
                        worksheet.Cells[row, 6].Value = createdDate;
                        worksheet.Cells[row, 6].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 6].Value = "N/A";
                    }

                    // Ensure Modified field is correctly formatted as DateTime in Excel
                    if (item["Modified"] != DBNull.Value)
                    {
                        DateTime modifiedDate = Convert.ToDateTime(item["Modified"]);
                        worksheet.Cells[row, 7].Value = modifiedDate;
                        worksheet.Cells[row, 7].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 7].Value = "N/A";
                    }


                    worksheet.Cells[row, 5].Value = item["UserName"];

                    row++;
                }


                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"QuizData-{DateTime.Now:G}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        #endregion
    }
}
