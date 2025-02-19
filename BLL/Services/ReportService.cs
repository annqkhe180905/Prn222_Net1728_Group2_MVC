using BLL.Dtos;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly INewsArticleRepository _newsArticleRepository;


        public ReportService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public async Task<ReportDto> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var allNews = await _newsArticleRepository.GetAllNewsByDate(startDate, endDate);

            var newsByCategory = allNews
                .Where(n => n.Category != null)
                .GroupBy(n => n.Category.CategoryName ?? "Unknown")
                .ToDictionary(g => g.Key, g => g.Count());

            var newsByCreator = allNews
                .Where(n => n.CreatedBy != null)
                .GroupBy(n => new { Name = n.CreatedBy.AccountName ?? "Unknown", Email = n.CreatedBy.AccountEmail ?? "No Email" })
                .ToDictionary(g => g.Key.Name, g => (g.Key.Email, g.Count()));

            return new ReportDto
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalNews = allNews.Count,
                NewsByStatus = new Dictionary<string, int>
                {
                    { "Active", allNews.Count(n => n.NewsStatus == true) },
                    { "Inactive", allNews.Count(n => n.NewsStatus == false) }
                },
                NewsByCategory = newsByCategory,
                NewsByCreator = newsByCreator
            };
        }
    }
}