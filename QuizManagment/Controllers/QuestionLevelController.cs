using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QuizManagment.Filters;
using QuizManagment.Models;
using System.Data;
using System.Data.SqlClient;


namespace QuizManagment.Controllers
{
    [CheckAccess]
    public class QuestionLevelController : Controller
    {
        #region Configuration

        private readonly IConfiguration _configuration;

        public QuestionLevelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Index

        // Index action to load all question levels
        public IActionResult Index()
        {
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_QuestionLevel_SelectAll", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading question levels: " + ex.Message;
                return View(new DataTable()); // Return an empty DataTable in case of error
            }

            return View(table.Rows.Count > 0 ? table : new DataTable());
        }

        #endregion

        #region Delete

        // Delete action to remove a question level by ID
        public IActionResult Delete(int QuestionLevelID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_QuestionLevel_DeleteByPK", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = QuestionLevelID;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting question level: " + ex.Message;
            }
            TempData["SuccessMessage"] = "Question level Deleted successfully!";
            return RedirectToAction("Index");
        }

        #endregion

        #region Question Level Add/Edit

        // Add or Edit question level
        public IActionResult QuestionLevelAddEdit(QuestionLevel model)
        {
            if (!ModelState.IsValid)
            {
                UserDropDown(); // Ensure dropdown is repopulated
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

                    if (model.QuestionLevelID == 0)
                    {
                        command.CommandText = "PR_QuestionLevel_Insert";
                    }
                    else
                    {
                        command.CommandText = "PR_QuestionLevel_UpdateByPK";
                        command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                        command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    }

                    command.Parameters.Add("@QuestionLevel", SqlDbType.VarChar).Value = model.Level;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;


                    command.ExecuteNonQuery();
                }
                // Add success message
                TempData["SuccessMessage"] = "Question level saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving question level: " + ex.Message;
                UserDropDown(); // Repopulate dropdown before returning
                return View("Form", model);
            }
        }


        #endregion

        #region Form
        // Form to add or edit question level
        public IActionResult Form(int? QuestionLevelID)
        {
            UserDropDown(); // Populate user dropdown

            if (QuestionLevelID == null)
            {
                var model = new QuestionLevel { Created = DateTime.Now };
                return View("Form", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_QuestionLevel_SelectByPK", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@QuestionLevelID", QuestionLevelID);
                    using (var reader = command.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);

                        var model = new QuestionLevel();
                        foreach (DataRow dataRow in table.Rows)
                        {
                            model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"]);
                            model.Level = dataRow["QuestionLevel"].ToString();
                            model.UserID = Convert.ToInt32(dataRow["UserID"]);
                        }

                        return View("Form", model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading question level: " + ex.Message;
                return View("Form", new QuestionLevel());
            }
        }

        #endregion

        #region User DropDown

        // Populate the user dropdown list
        private void UserDropDown()
        {
            // Initialize with empty list to prevent null
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

                        // Update with fetched data
                        ViewBag.UserList = userList;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading users: " + ex.Message;
                // ViewBag.UserList remains initialized as an empty list
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
            sqlCommand.CommandText = "PR_QuestionLevel_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuestionLevelID";
                worksheet.Cells[1, 2].Value = "QuestionLevel";
                worksheet.Cells[1, 3].Value = "UserName";
                worksheet.Cells[1, 4].Value = "Created";
                worksheet.Cells[1, 5].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuestionLevelID"];
                    worksheet.Cells[row, 2].Value = item["QuestionLevel"];
                    worksheet.Cells[row, 3].Value = item["UserName"];
                    // Ensure Created field is correctly formatted as DateTime in Excel
                    if (item["Created"] != DBNull.Value)
                    {
                        DateTime createdDate = Convert.ToDateTime(item["Created"]);
                        worksheet.Cells[row, 4].Value = createdDate;
                        worksheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 4].Value = "N/A";
                    }

                    // Ensure Modified field is correctly formatted as DateTime in Excel
                    if (item["Modified"] != DBNull.Value)
                    {
                        DateTime modifiedDate = Convert.ToDateTime(item["Modified"]);
                        worksheet.Cells[row, 5].Value = modifiedDate;
                        worksheet.Cells[row, 5].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 5].Value = "N/A";
                    }

                    row++;
                }


                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"QuestionLevelData-{DateTime.Now:G}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
        #endregion
    }
}
