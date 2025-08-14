using Microsoft.AspNetCore.Mvc;

namespace Extra2.Properties
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
