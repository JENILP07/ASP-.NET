using Microsoft.AspNetCore.Mvc;

namespace QuizManagment.Controllers
{
    public class QuizRendering : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
