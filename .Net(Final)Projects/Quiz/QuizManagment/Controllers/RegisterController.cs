using Microsoft.AspNetCore.Mvc;

namespace QuizManagment.Controllers
{
    public class RegisterController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
