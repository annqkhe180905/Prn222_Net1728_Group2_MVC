using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;

namespace Net1728Group2MVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService newsTagService)
        {
            _tagService = newsTagService;
        }

        public async Task<IActionResult> Index()
        {
            var newsTags =  _tagService.GetTags();
            return View("~/Views/Tag/Index.cshtml", newsTags);
        }

        public IActionResult Create()
        {
            return View("~/Views/Tag/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag newsTag)
        {
            if (ModelState.IsValid)
            {
                 _tagService.CreateTag(newsTag);
                return RedirectToAction(nameof(Index));
            }
            return View($"~/Views/Tag/{nameof(newsTag)}.cshtml", newsTag);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var newsTag = await _tagService.GetTags().FirstOrDefault(t => t.TagId == id);
   
            if (newsTag == null)
            {
                return NotFound();
            }
            return View($"~/Views/Tag/{nameof(newsTag)}.cshtml", newsTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tag newsTag)
        {
            if (id != newsTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _tagService.UpdateTag(newsTag);
                return RedirectToAction(nameof(Index));
            }
            return View($"~/Views/Tag/{nameof(newsTag)}.cshtml", newsTag);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var newsTag = await _tagService.DeleteTag(id);
            if (newsTag == null)
            {
                return NotFound();
            }
            return View($"~/Views/Tag/{nameof(newsTag)}.cshtml", newsTag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tagService.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
