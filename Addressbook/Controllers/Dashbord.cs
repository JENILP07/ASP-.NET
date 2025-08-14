using Microsoft.AspNetCore.Mvc;

namespace Addressbook.Controllers
{
    public class Dashbord : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
