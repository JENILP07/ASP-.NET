using Microsoft.AspNetCore.Mvc;

namespace Addressbook.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
