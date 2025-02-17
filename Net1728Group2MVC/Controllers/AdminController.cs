using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Account()
        {
            ViewBag.Header = "Account Management";
            return View();
        }

        public IActionResult Report()
        {
            ViewBag.Header = "Report Management";

            return View();
        }
    }
}
