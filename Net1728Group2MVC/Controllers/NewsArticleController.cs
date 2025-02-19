using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;

        public NewsArticleController(INewsArticleService newsArticleService, IMapper mapper)
        {
            _newsArticleService = newsArticleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsArticle(NewsArticleControllerModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var news = _mapper.Map<NewsArticleVM>(model);
            await _newsArticleService.UpdateNewsArticle(news);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateNewsArticle(NewsArticleControllerModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var news = _mapper.Map<NewsArticleVM>(model);
            await _newsArticleService.UpdateNewsArticle(news);
            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNewsArticle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID");
            }

            bool isDeleted = await _newsArticleService.DeleteNewsArticle(id);
            if (!isDeleted)
            {
                return NotFound("News article is not found");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsArticle()
        {
            var newsList = await _newsArticleService.GetAllArticle();
            return View(newsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveNewsArticle()
        {
            var newsList = await _newsArticleService.GetAllActiveArticles();
            return View(newsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsArticleById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid news article!");
            }

            var news = await _newsArticleService.GetNewsArticleById(id);
            if (news == null)
            {
                return NotFound("News article not found!");
            }

            return View(news);
        }
    }
}
