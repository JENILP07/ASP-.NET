using Microsoft.AspNetCore.Mvc;
using QuizManagment.Models;
using System.Data;
using System.Data.SqlClient;

namespace QuizManagment.Controllers
{
    public class QuestionController : Controller
    {
        #region Configuration
        private readonly IConfiguration _configuration;

        public QuestionController(IConfiguration configuration)
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
                    using (SqlCommand command = new SqlCommand("PR_Question_SelectAll", connection))
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
                ViewBag.ErrorMessage = "Error loading questions: " + ex.Message;
                return View(new DataTable());
            }

            return View(table.Rows.Count > 0 ? table : new DataTable());
        }
        #endregion

        #region Delete
        public IActionResult Delete(int QuestionID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_Question_DeleteByPK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@QuestionID", QuestionID);
                        command.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Question deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting question: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Form
        [HttpGet]
        public IActionResult Form(int? QuestionID)
        {
            PopulateDropdowns();
            if (QuestionID == null)
            {
                return View(new Question { Created = DateTime.Now, Modified = DateTime.Now, IsActive = true });
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("PR_Question_SelectByPK", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@QuestionID", QuestionID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            var model = new Question
                            {
                                QuestionID = row.Field<int>("QuestionID"),
                              
                                QuestionText = row.Field<string>("QuestionText"),
                                QuestionLevelID = row.Field<int>("QuestionLevelID"),
                                OptionA = row.Field<string>("OptionA"),
                                OptionB = row.Field<string>("OptionB"),
                                OptionC = row.Field<string>("OptionC"),
                                OptionD = row.Field<string>("OptionD"),
                                CorrectOption = row.Field<string>("CorrectOption"),
                                QuestionMarks = row.Field<int>("QuestionMarks"),
                                UserID = row.Field<int>("UserID"),
                                IsActive = row.Field<bool>("IsActive"),
                                Created = row.Field<DateTime>("Created"),
                                Modified = row.Field<DateTime>("Modified")
                            };
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading question: " + ex.Message;
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Question not found.";
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEditQuestion
        [HttpPost]
        public IActionResult AddEditQuestion(Question model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View("Form", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    if (model.QuestionID == 0) // Insert new question
                    {
                        TempData["SuccessMessage"] = "Question inserted successfully!";
                        command.CommandText = "PR_Question_Insert";
                    }
                    else // Update existing question
                    {
                        TempData["SuccessMessage"] = "Question updated successfully!";
                        command.CommandText = "PR_Question_UpdateByPK";
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@Modified", DateTime.UtcNow);
                    }

                    // Add parameters
                    command.Parameters.AddWithValue("@QuestionText", model.QuestionText);
                    command.Parameters.AddWithValue("@QuestionLevelID", model.QuestionLevelID);
                    command.Parameters.AddWithValue("@OptionA", model.OptionA);
                    command.Parameters.AddWithValue("@OptionB", model.OptionB);
                    command.Parameters.AddWithValue("@OptionC", model.OptionC ?? (object)DBNull.Value); // Handle NULL
                    command.Parameters.AddWithValue("@OptionD", model.OptionD ?? (object)DBNull.Value); // Handle NULL
                    command.Parameters.AddWithValue("@CorrectOption", model.CorrectOption);
                    command.Parameters.AddWithValue("@QuestionMarks", model.QuestionMarks);
                    command.Parameters.AddWithValue("@UserID", model.UserID);
                    command.Parameters.AddWithValue("@IsActive", model.IsActive);

                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving question: " + ex.Message;
                PopulateDropdowns();
                return View("Form", model);
            }
        }
        #endregion

        #region Dropdowns
        private void PopulateDropdowns()
        {
            QuizDropDown();
            QuestionLevelDropDown();
            UserDropDown();
        }

        private void QuizDropDown()
        {
            ViewBag.QuizList = new List<QuizDropDown>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("PR_QUIZ_DROPDOWN", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    ViewBag.QuizList = dt.AsEnumerable().Select(row => new QuizDropDown
                    {
                        QuizID = row.Field<int>("QuizID"),
                        QuizName = row.Field<string>("QuizName")
                    }).ToList();
                }
            }
        }

        private void QuestionLevelDropDown()
        {
            ViewBag.QuestionLevelList = new List<QuestionLevelDropDown>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("PR_QUESTIONLEVEL_DROPDOWN", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    ViewBag.QuestionLevelList = dt.AsEnumerable().Select(row => new QuestionLevelDropDown
                    {
                        QuestionLevelID = row.Field<int>("QuestionLevelID"),
                        LevelName = row.Field<string>("QuestionLevel")
                    }).ToList();
                }
            }
        }

        private void UserDropDown()
        {
            ViewBag.UserList = new List<UserDropDown>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("PR_USER_DROPDOWN", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    ViewBag.UserList = dt.AsEnumerable().Select(row => new UserDropDown
                    {
                        UserID = row.Field<int>("UserID"),
                        UserName = row.Field<string>("UserName")
                    }).ToList();
                }
            }
        }
        #endregion
    }
}