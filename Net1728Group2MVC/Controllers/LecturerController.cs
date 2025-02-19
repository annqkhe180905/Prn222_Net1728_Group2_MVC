using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class LecturerController : BaseController
    {
        public IActionResult News()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
