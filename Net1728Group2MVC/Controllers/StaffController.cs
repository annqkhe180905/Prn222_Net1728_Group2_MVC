using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;
        public StaffController(ICategoryService categoryService, IMapper mapper, INewsArticleService newsArticleService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _newsArticleService = newsArticleService;
        }
        public async Task<IActionResult> News()
        {
            ViewBag.Header = "News Management";
            var newsArticles = await _newsArticleService.GetAllArticle();
            var newsArticleControllerModel = new NewsArticleControllerModel()
            {
                NewsArticles = _mapper.Map<IEnumerable<NewsArticleVM>>(newsArticles)
            };
            return View(newsArticleControllerModel);
        }
        public async Task<IActionResult> Category(string? search)
        {
            ViewBag.Header = "Category Management";
            var categoryModal = new CategoryModal()
            {
                Categories = await _categoryService.GetAllAsync(search)
            };

            return View(categoryModal);
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
