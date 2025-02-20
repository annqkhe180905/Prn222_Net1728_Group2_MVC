using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;
using System.Security.Claims;
using System.Text.Json;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Net1728Group2MVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public NewsArticleController(INewsArticleService newsArticleService, IMapper mapper, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _mapper = mapper;
            _tagService = tagService;
        }

        public async Task<IActionResult> Search(string? search, int? categoryId, List<int>? tagIds, short? createdBy)
        {
            var jsonString = HttpContext.Session.GetString("User");
            var user = JsonSerializer.Deserialize<SystemAccount>(jsonString);
            var model = await _newsArticleService.SearchArticles(search, categoryId, tagIds, user.AccountId);
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var article = await _newsArticleService.GetNewsArticleById(id);
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsArticle(NewsArticleVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("News", "Staff");
            }

            var existingArticle = await _newsArticleService.GetNewsArticleById(model.NewsArticleId);
            if (existingArticle != null)
            {
                ModelState.AddModelError("NewsArticleId", "NewsArticleId already exists.");
                return RedirectToAction("News", "Staff");
            }


            var jsonString = HttpContext.Session.GetString("User");
            var user = JsonSerializer.Deserialize<SystemAccount>(jsonString);
            model.CreatedById = user.AccountId;
            await _newsArticleService.CreateNewsArticle(model);
            return RedirectToAction("News", "Staff");
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateNewsArticle(NewsArticleControllerModel model)
        {
            var jsonString = HttpContext.Session.GetString("User");
            var user = JsonSerializer.Deserialize<SystemAccount>(jsonString);
            model.UpdatedById = user.AccountId;
            var news = new NewsArticleVM
            {
                NewsArticleId = model.NewsArticleId,
                NewsTitle = model.NewsTitle,
                Headline = model.Headline,
                NewsContent = model.NewsContent,
                NewsSource = model.NewsSource,
                CategoryId = model.CategoryId,
                NewsStatus = model.NewsStatus,
                UpdatedById = model.UpdatedById,
                TagIds = model.TagIds
            };
            await _newsArticleService.UpdateNewsArticle(news);
            return RedirectToAction("News", "Staff");
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
                return NotFound("Cannot find this news!");
            }

            return RedirectToAction("News", "Staff");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsArticle()
        {
            var newsList = await _newsArticleService.GetAllArticles();
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
