using Microsoft.AspNetCore.Mvc;

namespace Addressbook.Controllers
{
    public class City : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
