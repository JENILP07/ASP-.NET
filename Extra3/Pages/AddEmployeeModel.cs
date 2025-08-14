using Microsoft.AspNetCore.Mvc;

namespace Extra3.Pages
{
    public class AddEmployeeModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
