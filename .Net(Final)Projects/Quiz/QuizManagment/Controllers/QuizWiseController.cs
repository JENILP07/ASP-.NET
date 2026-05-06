using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizManagment.Models;
using System.Data;

namespace QuizManagment.Controllers
{
    [Authorize]
    public class QuizWiseController : Controller
    {
        private readonly IConfiguration _configuration;
        public QuizWiseController(IConfiguration configuration) { _configuration = configuration; }

        private string GetConnectionString() =>
            _configuration.GetConnectionString("ConnectionString")
            ?? throw new InvalidOperationException("Connection string not found.");

        public IActionResult Index()
        {
            DataTable table = new DataTable();
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PR_QuizWiseQuestions_SelectAll", conn) { CommandType = CommandType.StoredProcedure };
                using var reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading quiz wise questions: " + ex.Message;
                return View(new DataTable());
            }
            return View(table.Rows.Count > 0 ? table : new DataTable());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult QuizWiseQuestionAddEdit(QuizWiseQuestion model)
        {
            if (model == null) return BadRequest("Invalid data received.");
            if (!ModelState.IsValid) { PopulateDropdowns(); return View("Form", model); }
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (model.QuizWiseQuestionsID == 0)
                    cmd.CommandText = "PR_QuizWiseQuestions_Insert";
                else
                {
                    cmd.CommandText = "PR_QuizWiseQuestions_UpdateByPK";
                    cmd.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                }
                cmd.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                cmd.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                cmd.ExecuteNonQuery();
                TempData["SuccessMessage"] = "Quiz wise question saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                PopulateDropdowns();
                return View("Form", model);
            }
        }

        public IActionResult Form(int? QuizWiseQuestionsID)
        {
            PopulateDropdowns();
            var model = new QuizWiseQuestion();
            if (QuizWiseQuestionsID != null)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    using var cmd = new SqlCommand("PR_QuizWiseQuestions_SelectByPK", conn) { CommandType = CommandType.StoredProcedure };
                    conn.Open();
                    cmd.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
                    using var reader = cmd.ExecuteReader();
                    var table = new DataTable(); table.Load(reader);
                    if (table.Rows.Count > 0)
                    {
                        var dr = table.Rows[0];
                        model.QuizWiseQuestionsID = Convert.ToInt32(dr["QuizWiseQuestionsID"]);
                        model.QuizID = Convert.ToInt32(dr["QuizID"]);
                        model.QuestionID = Convert.ToInt32(dr["QuestionID"]);
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                    }
                    else TempData["ErrorMessage"] = "No data found.";
                }
                catch (Exception ex) { TempData["ErrorMessage"] = "Error: " + ex.Message; }
            }
            return View("Form", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int QuizWiseQuestionsID)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = new SqlCommand("PR_QuizWiseQuestions_DeleteByPK", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
                cmd.ExecuteNonQuery();
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            catch (Exception ex) { TempData["ErrorMessage"] = "Error deleting: " + ex.Message; }
            return RedirectToAction("Index");
        }

        #region Dropdowns
        private void PopulateDropdowns() { QuizDropDown(); QuestionLevelDropDown(); UserDropDown(); }

        private void QuizDropDown()
        {
            ViewBag.QuizList = new List<QuizDropDown>();
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("PR_QUIZ_DROPDOWN", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using var reader = cmd.ExecuteReader();
            var dt = new DataTable(); dt.Load(reader);
            ViewBag.QuizList = dt.AsEnumerable().Select(r => new QuizDropDown { QuizID = r.Field<int>("QuizID"), QuizName = r.Field<string>("QuizName") ?? "" }).ToList();
        }

        private void QuestionLevelDropDown()
        {
            ViewBag.QuestionLevelList = new List<QuestionLevelDropDown>();
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("PR_QUESTIONLEVEL_DROPDOWN", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using var reader = cmd.ExecuteReader();
            var dt = new DataTable(); dt.Load(reader);
            ViewBag.QuestionLevelList = dt.AsEnumerable().Select(r => new QuestionLevelDropDown { QuestionLevelID = r.Field<int>("QuestionLevelID"), LevelName = r.Field<string>("QuestionLevel") ?? "" }).ToList();
        }

        private void UserDropDown()
        {
            ViewBag.UserList = new List<UserDropDown>();
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("PR_USER_DROPDOWN", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using var reader = cmd.ExecuteReader();
            var dt = new DataTable(); dt.Load(reader);
            ViewBag.UserList = dt.AsEnumerable().Select(r => new UserDropDown { UserID = r.Field<int>("UserID"), UserName = r.Field<string>("UserName") ?? "" }).ToList();
        }
        #endregion
    }
}
