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
        public async Task<IActionResult> NewsTag(string? search)
        {
            
            var tags = await _tagService.GetAllTagsAsync(search);
            var tagModel = new TagModel { Tag = tags.ToList() };
            return View(tagModel);
        }

    }
}
