using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }
    }
}
