using System.Diagnostics;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net1728Group2MVC.Models;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, INewsArticleService newsArticleService, IMapper mapper)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var newsArticles = await _newsArticleService.GetAllActiveArticles();

            var newsArticleVMs = _mapper.Map<IEnumerable<NewsArticleVM>>(newsArticles);

            var newsArticleControllerModel = new NewsArticleControllerModel
            {
                NewsArticles = newsArticleVMs.Select(article => new NewsArticleVM
                {
                    NewsArticleId = article.NewsArticleId,
                    NewsTitle = article.NewsTitle,
                    Headline = article.Headline,
                    CreatedDate = article.CreatedDate,
                    NewsContent = article.NewsContent,
                    NewsSource = article.NewsSource,
                    CategoryId = article.CategoryId,
                    NewsStatus = article.NewsStatus,
                    CreatedById = article.CreatedById,
                    UpdatedById = article.UpdatedById,
                    ModifiedDate = article.ModifiedDate,
                    CategoryName = article.CategoryName, 
                    CreatedBy = article.CreatedBy, 
                    TagIds = article.TagIds
                }).ToList()
            };

            return View(newsArticleControllerModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var article = await _newsArticleService.GetNewsArticleById(id);

            if (article == null)
            {
                return NotFound();
            }
            var model = new NewsArticleControllerModel
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryName = article.Category?.CategoryName,
                NewsStatus = article.NewsStatus,
                CreatedBy = article.CreatedBy?.AccountName,
                ModifiedDate = article.ModifiedDate
            };

            return PartialView("Detail", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
