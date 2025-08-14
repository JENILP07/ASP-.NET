using Microsoft.AspNetCore.Mvc;
using QuizManagment.Filters;

namespace QuizManagment.Controllers
{
    [CheckAccess]
    public class QuizRendering : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
