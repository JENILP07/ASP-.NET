using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QuizManagment.Models;
using System.Data;
using System.Data.SqlClient;

namespace QuizManagment.Controllers
{
    public class UserController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
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
                    using (SqlCommand command = new SqlCommand("PR_User_SelectAll", connection))
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
                ViewBag.ErrorMessage = "Error loading users: " + ex.Message;
                return View(new DataTable()); // Ensure an empty DataTable is returned
            }

            return View(table.Rows.Count > 0 ? table : new DataTable());
        }

        #endregion

        #region Delete

        public IActionResult Delete(int UserID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_User_DeleteByPK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting user: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Add/Edit Form
        public IActionResult UserAddEdit(User model)
        {
            if (!ModelState.IsValid)
            {
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

                    if (model.UserID == 0) // Insert new user
                    {
                        TempData["SuccessMessage"] = "User Inserted successfully!";
                        command.CommandText = "PR_User_Insert";
                    }
                    else // Update existing user
                    {
                        TempData["SuccessMessage"] = "User Updated successfully!";
                        command.CommandText = "PR_User_UpdateByPK";
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                        command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    }

                    // Add parameters
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = model.UserName;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = model.Password;
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email;
                    command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                    command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = model.IsAdmin;

                    command.ExecuteNonQuery();
                }

                // Add success message

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving user: " + ex.Message;
                return View("Form", model);
            }
        }



        #endregion

        #region Form

        // Add or Edit User Form
        public IActionResult Form(int? UserID)
        {
            if (UserID == null)
            {
                var model = new User { Created = DateTime.Now, Modified = DateTime.Now, IsActive = true, IsAdmin = false }; // Default values
                return View("Form", model); // New User form
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("PR_User_SelectByPK", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    using (var reader = command.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);

                        if (table.Rows.Count > 0)
                        {
                            var dataRow = table.Rows[0];
                            var model = new User
                            {
                                UserID = Convert.ToInt32(dataRow["UserID"]),
                                UserName = dataRow["UserName"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Mobile = dataRow["Mobile"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                IsActive = Convert.ToBoolean(dataRow["IsActive"]),
                                IsAdmin = Convert.ToBoolean(dataRow["IsAdmin"]),
                                Created = Convert.ToDateTime(dataRow["Created"]),
                                Modified = Convert.ToDateTime(dataRow["Modified"]),
                            };

                            return View("Form", model); // Edit User form
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading user: " + ex.Message;
                return View("Form", new User());
            }

            return RedirectToAction("Index");
        }


        #endregion

        #region Excel
        public IActionResult ExportToExcel()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_User_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "UserName";
                worksheet.Cells[1, 2].Value = "Password";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Mobile";
                worksheet.Cells[1, 5].Value = "IsActive";
                worksheet.Cells[1, 6].Value = "IsAdmin";
                worksheet.Cells[1, 7].Value = "Creation";
                worksheet.Cells[1, 8].Value = "Modified";

                // Add data               

                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["UserName"];
                    worksheet.Cells[row, 2].Value = item["Password"];
                    worksheet.Cells[row, 3].Value = item["Email"];
                    worksheet.Cells[row, 4].Value = item["Mobile"];
                    worksheet.Cells[row, 5].Value = item["IsActive"];
                    worksheet.Cells[row, 6].Value = item["IsAdmin"];
                    // Ensure Created field is correctly formatted as DateTime in Excel
                    if (item["Created"] != DBNull.Value)
                    {
                        DateTime createdDate = Convert.ToDateTime(item["Created"]);
                        worksheet.Cells[row, 7].Value = createdDate;
                        worksheet.Cells[row, 7].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 7].Value = "N/A";
                    }

                    // Ensure Modified field is correctly formatted as DateTime in Excel
                    if (item["Modified"] != DBNull.Value)
                    {
                        DateTime modifiedDate = Convert.ToDateTime(item["Modified"]);
                        worksheet.Cells[row, 8].Value = modifiedDate;
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; // Format date and time in Excel
                    }
                    else
                    {
                        worksheet.Cells[row, 8].Value = "N/A";
                    }

                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"UserData-{DateTime.Now:G}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
        #endregion
    }
}
