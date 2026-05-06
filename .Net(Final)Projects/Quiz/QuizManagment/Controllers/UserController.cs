using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using QuizManagment.Models;
using System.Data;

namespace QuizManagment.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration) { _configuration = configuration; }

        private string GetConnectionString() =>
            _configuration.GetConnectionString("ConnectionString")
            ?? throw new InvalidOperationException("Connection string not found.");

        #region Index
        public IActionResult Index()
        {
            DataTable table = new DataTable();
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PR_User_SelectAll", conn) { CommandType = CommandType.StoredProcedure };
                using var reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading users: " + ex.Message;
                return View(new DataTable());
            }
            return View(table.Rows.Count > 0 ? table : new DataTable());
        }
        #endregion

        #region Delete
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int UserID)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PR_User_DeleteByPK", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            catch (Exception ex) { TempData["ErrorMessage"] = "Error deleting user: " + ex.Message; }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UserAddEdit(User model)
        {
            if (!ModelState.IsValid) return View("Form", model);
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                using var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.UserID == 0)
                {
                    cmd.CommandText = "PR_User_Insert";
                    // Hash password for new users
                    model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    TempData["SuccessMessage"] = "User inserted successfully!";
                }
                else
                {
                    cmd.CommandText = "PR_User_UpdateByPK";
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                    cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    // Only hash if password was changed (not already a hash)
                    if (!model.Password.StartsWith("$2"))
                        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    TempData["SuccessMessage"] = "User updated successfully!";
                }

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = model.UserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = model.Password;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email;
                cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = model.IsAdmin;
                cmd.ExecuteNonQuery();
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
        public IActionResult Form(int? UserID)
        {
            if (UserID == null)
                return View("Form", new User { Created = DateTime.Now, Modified = DateTime.Now, IsActive = true, IsAdmin = false });

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                using var cmd = new SqlCommand("PR_User_SelectByPK", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                cmd.Parameters.AddWithValue("@UserID", UserID);
                using var reader = cmd.ExecuteReader();
                var table = new DataTable(); table.Load(reader);
                if (table.Rows.Count > 0)
                {
                    var dr = table.Rows[0];
                    var model = new User
                    {
                        UserID = Convert.ToInt32(dr["UserID"]),
                        UserName = dr["UserName"].ToString() ?? "",
                        Email = dr["Email"].ToString() ?? "",
                        Mobile = dr["Mobile"].ToString() ?? "",
                        Password = dr["Password"].ToString() ?? "",
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        IsAdmin = Convert.ToBoolean(dr["IsAdmin"]),
                        Created = Convert.ToDateTime(dr["Created"]),
                        Modified = Convert.ToDateTime(dr["Modified"]),
                    };
                    return View("Form", model);
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

        #region Excel — Password column REMOVED for security
        public IActionResult ExportToExcel()
        {
            using var sqlConn = new SqlConnection(GetConnectionString());
            sqlConn.Open();
            using var sqlCmd = sqlConn.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "PR_User_SelectAll";
            using var sqlReader = sqlCmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlReader);

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("DataSheet");

            // Headers — NO password column
            ws.Cells[1, 1].Value = "UserName";
            ws.Cells[1, 2].Value = "Email";
            ws.Cells[1, 3].Value = "Mobile";
            ws.Cells[1, 4].Value = "IsActive";
            ws.Cells[1, 5].Value = "IsAdmin";
            ws.Cells[1, 6].Value = "Created";
            ws.Cells[1, 7].Value = "Modified";

            int row = 2;
            foreach (DataRow item in data.Rows)
            {
                ws.Cells[row, 1].Value = item["UserName"];
                ws.Cells[row, 2].Value = item["Email"];
                ws.Cells[row, 3].Value = item["Mobile"];
                ws.Cells[row, 4].Value = item["IsActive"];
                ws.Cells[row, 5].Value = item["IsAdmin"];
                if (item["Created"] != DBNull.Value)
                {
                    ws.Cells[row, 6].Value = Convert.ToDateTime(item["Created"]);
                    ws.Cells[row, 6].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                }
                if (item["Modified"] != DBNull.Value)
                {
                    ws.Cells[row, 7].Value = Convert.ToDateTime(item["Modified"]);
                    ws.Cells[row, 7].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                }
                row++;
            }
            var stream = new MemoryStream(); package.SaveAs(stream); stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"UserData-{DateTime.Now:G}.xlsx");
        }
        #endregion
    }
}
