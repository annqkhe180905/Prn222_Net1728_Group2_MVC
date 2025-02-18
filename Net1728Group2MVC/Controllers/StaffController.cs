using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ITagService _tagService;
        public StaffController(ITagService tagServcie)
        {
            _tagService = tagServcie;
        }

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
        public IActionResult NewsTag()
        {
            var newsTags = _tagService.GetAllTags();
            if (newsTags != null)
            {
                return NotFound();
            }
            return View(newsTags);
        }

    }
}
