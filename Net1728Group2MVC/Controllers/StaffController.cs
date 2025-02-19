using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;

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
        public async Task<IActionResult> NewsTag()
        {
            ViewBag.Header = "News Tag Management";
            var tagModel = new TagModel { 
                Tag = await _tagService.GetAllTagsAsync()
            };
            return View(tagModel);
        }

    }
}
