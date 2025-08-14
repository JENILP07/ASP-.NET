using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using QuizManagment.Models;

namespace QuizManagment.Controllers
{
    public class QuizWiseController : Controller
    {
        private readonly IConfiguration _configuration;

        public QuizWiseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_QuizWiseQuestions_SelectAll", connection))
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
                ViewBag.ErrorMessage = "Error loading question levels: " + ex.Message;
                return View(new DataTable());
            }

            return View(table.Rows.Count > 0 ? table : new DataTable());
        }
        public IActionResult QuizWiseQuestionAddEdit(QuizWiseQuestion model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data received.");
            }

            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Database connection string is missing.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (model.QuizWiseQuestionsID == 0)
                        {
                            command.CommandText = "PR_QuizWiseQuestions_Insert";
                        }
                        else
                        {
                            command.CommandText = "PR_QuizWiseQuestions_UpdateByPK";
                            command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                        }

                        command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                        command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving data: " + ex.Message);
                return View("Form", model);
            }
        }

        #region AddForm
        // Display the form for adding or editing a quiz
        public IActionResult Form(int? QuizWiseQuestionsID)
        {
            PopulateDropdowns(); // Ensure dropdowns are set

            var model = new QuizWiseQuestion(); // Always initialize the model

            if (QuizWiseQuestionsID != null)
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("PR_QuizWiseQuestions_SelectByPK", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);

                        using (var reader = command.ExecuteReader())
                        {
                            var table = new DataTable();
                            table.Load(reader);

                            if (table.Rows.Count > 0) // Ensure we have data
                            {
                                var dataRow = table.Rows[0];
                                model.QuizWiseQuestionsID = Convert.ToInt32(dataRow["QuizWiseQuestionsID"]);
                                model.QuizID = Convert.ToInt32(dataRow["QuizID"]);
                                model.QuestionID = Convert.ToInt32(dataRow["QuestionID"]);
                                model.UserID = Convert.ToInt32(dataRow["UserID"]);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "No data found for the given QuizWiseQuestionsID.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error loading quiz details: " + ex.Message;
                }
            }

            return View("Form", model); // Ensure model is always returned
        }


        #endregion


        public IActionResult Delete(int QuizWiseQuestionsID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_QuizWiseQuestions_DeleteByPK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting user: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

   /*     public IActionResult AddForm()
        {
            UserDropDown();
            QuestionDropDown();
            QuizDropDown();
            return View("Form");
        }

        public IActionResult EditForm()
        {
            UserDropDown();
            QuestionLevelDropDown();
            return View("Form");
        }*/


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

