using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using BLL.DTOs;

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
            var newsTags = _tagService.GetAllTags();
            return View(newsTags);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("~/Views/Tag/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagVM tagVM)
        {
            if (ModelState.IsValid)
            {
                 _tagService.CreateTag(tagVM);
                return RedirectToAction(nameof(Index));
            }
            return View($"~/Views/Tag/{nameof(tagVM)}.cshtml", tagVM);
        }

    }
}
