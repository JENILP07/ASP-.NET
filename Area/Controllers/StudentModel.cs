using Microsoft.AspNetCore.Mvc;

namespace Area.Controllers
{
    public class StudentModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
