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
                var news = _mapper.Map<NewsArticleVM>(model);
                await _newsArticleService.CreateNewsArticle(news);
                return RedirectToAction("Index", "Home");
            }
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateNewsArticle(NewsArticleControllerModel model)
        {
            if (ModelState.IsValid)
            {
                var news = _mapper.Map<NewsArticleVM>(model);
                await _newsArticleService.UpdateNewsArticle(news);
                return RedirectToAction("Index", "Home");
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNewsArticle(NewsArticleControllerModel model)
        {
            if (ModelState.IsValid)
            {
                var news = _mapper.Map<NewsArticleVM>(model);
                await _newsArticleService.UpdateNewsArticle(news);
                return RedirectToAction("Index", "Home");
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsArticle()
        {
            if (ModelState.IsValid)
            {
                var newsList = await _newsArticleService.GetAllArticle();
                
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveNewsArticle()
        {
            if (ModelState.IsValid)
            {
                var newsList = await _newsArticleService.GetAllActiveArticles();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsArticleById(string id)
        {
            if (ModelState.IsValid)
            {
                var newsList = await _newsArticleService.GetNewsArticleById(id);
            }
            return View();
        }
    }
}
