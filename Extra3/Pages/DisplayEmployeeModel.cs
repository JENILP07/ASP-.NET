using Microsoft.AspNetCore.Mvc;

namespace Extra3.Pages
{
    public class DisplayEmployeeModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
