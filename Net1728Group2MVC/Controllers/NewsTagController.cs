using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class NewsTagController : Controller
    {
        private readonly ITagService _tagService;
        public NewsTagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagVM tagVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool succeed = await _tagService.CreateTagAsync(tagVM);
            return succeed ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagVM tagVM)
        {
            bool succeed = await _tagService.UpdateTagAsync(tagVM);
            return succeed ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool succeed = await _tagService.DeleteTagAsync(id);
                return succeed ? Ok() : BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
