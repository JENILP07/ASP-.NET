using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using QuizManagment.Models;
using System.Data;

namespace QuizManagment.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;
        public QuizController(IConfiguration configuration) { _configuration = configuration; }
        #endregion

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
                using var cmd = new SqlCommand("PR_Quiz_SelectAll", conn) { CommandType = CommandType.StoredProcedure };
                using var reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading Quizzes: " + ex.Message;
                return View(new DataTable());
            }
            return View(table.Rows.Count > 0 ? table : new DataTable());
        }
        #endregion

        #region Delete
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int QuizID)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PR_Quiz_DeleteByPK", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@QuizID", QuizID);
                cmd.ExecuteNonQuery();
                TempData["SuccessMessage"] = "Quiz deleted successfully!";
            }
            catch (Exception ex) { TempData["ErrorMessage"] = "Error deleting quiz: " + ex.Message; }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add/Edit Quiz
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult QuizAddEdit(Quiz model)
        {
            if (!ModelState.IsValid) { UserDropDown(); return View("Form", model); }
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                using var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                if (model.QuizID == 0)
                {
                    cmd.CommandText = "PR_Quiz_Insert";
                    TempData["SuccessMessage"] = "Quiz inserted successfully!";
                }
                else
                {
                    cmd.CommandText = "PR_Quiz_UpdateByPK";
                    cmd.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                    cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    TempData["SuccessMessage"] = "Quiz updated successfully!";
                }
                cmd.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = model.QuizName;
                cmd.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                cmd.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving quiz: " + ex.Message;
                UserDropDown();
                return View("Form", model);
            }
        }
        #endregion

        #region Form
        public IActionResult Form(int? QuizID)
        {
            UserDropDown();
            if (QuizID == null) return View("Form", new Quiz { Created = DateTime.Now });
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                using var cmd = new SqlCommand("PR_Quiz_SelectByPK", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                cmd.Parameters.AddWithValue("@QuizID", QuizID);
                using var reader = cmd.ExecuteReader();
                var table = new DataTable();
                table.Load(reader);
                var model = new Quiz();
                foreach (DataRow dr in table.Rows)
                {
                    model.QuizID = Convert.ToInt32(dr["QuizID"]);
                    model.QuizName = dr["QuizName"].ToString() ?? "";
                    model.TotalQuestions = Convert.ToInt32(dr["TotalQuestions"]);
                    model.QuizDate = Convert.ToDateTime(dr["QuizDate"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                }
                return View("Form", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading quiz details: " + ex.Message;
                return View("Form", new Quiz());
            }
        }
        #endregion

        #region User DropDown
        private void UserDropDown()
        {
            ViewBag.UserList = new List<UserDropDown>();
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                using var cmd = new SqlCommand("PR_USER_DROPDOWN", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                using var reader = cmd.ExecuteReader();
                var dt = new DataTable(); dt.Load(reader);
                ViewBag.UserList = dt.AsEnumerable().Select(r => new UserDropDown
                {
                    UserID = Convert.ToInt32(r["UserID"]),
                    UserName = r["UserName"].ToString() ?? ""
                }).ToList();
            }
            catch (Exception ex) { TempData["ErrorMessage"] = "Error loading users: " + ex.Message; }
        }
        #endregion

        #region Excel
        public IActionResult ExportToExcel()
        {
            using var sqlConn = new SqlConnection(GetConnectionString());
            sqlConn.Open();
            using var sqlCmd = sqlConn.CreateCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "PR_Quiz_SelectAll";
            using var sqlReader = sqlCmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlReader);

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("DataSheet");
            ws.Cells[1, 1].Value = "Quiz ID"; ws.Cells[1, 2].Value = "Quiz Name";
            ws.Cells[1, 3].Value = "Total Question"; ws.Cells[1, 4].Value = "Quiz Date";
            ws.Cells[1, 5].Value = "User Name"; ws.Cells[1, 6].Value = "Created";
            ws.Cells[1, 7].Value = "Modified";

            int row = 2;
            foreach (DataRow item in data.Rows)
            {
                ws.Cells[row, 1].Value = item["QuizID"];
                ws.Cells[row, 2].Value = item["QuizName"];
                ws.Cells[row, 3].Value = item["TotalQuestions"];
                if (item["QuizDate"] != DBNull.Value) { ws.Cells[row, 4].Value = Convert.ToDateTime(item["QuizDate"]); ws.Cells[row, 4].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; }
                ws.Cells[row, 5].Value = item["UserName"];
                if (item["Created"] != DBNull.Value) { ws.Cells[row, 6].Value = Convert.ToDateTime(item["Created"]); ws.Cells[row, 6].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; }
                if (item["Modified"] != DBNull.Value) { ws.Cells[row, 7].Value = Convert.ToDateTime(item["Modified"]); ws.Cells[row, 7].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss"; }
                row++;
            }
            var stream = new MemoryStream(); package.SaveAs(stream); stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"QuizData-{DateTime.Now:G}.xlsx");
        }
        #endregion
    }
}
