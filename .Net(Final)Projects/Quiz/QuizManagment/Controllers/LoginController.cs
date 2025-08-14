using Microsoft.AspNetCore.Mvc;

namespace QuizManagment.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
