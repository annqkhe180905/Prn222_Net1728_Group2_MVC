using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
