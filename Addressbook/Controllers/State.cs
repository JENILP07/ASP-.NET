using Microsoft.AspNetCore.Mvc;

namespace Addressbook.Controllers
{
    public class State : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
