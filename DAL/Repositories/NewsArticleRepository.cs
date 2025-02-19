using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsManagementContext _dbContext;

        public NewsArticleRepository(FunewsManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NewsArticle> CreateNewsArticle(NewsArticle news)
        {
            try
            {
                _dbContext.NewsArticles.Add(news);
                await _dbContext.SaveChangesAsync();
                return news;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteNewsArticle(NewsArticle news)
        {
            _dbContext.NewsArticles.Remove(news);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllActiveArticles()
        {
            return await _dbContext.NewsArticles
                .Where(n => n.NewsStatus == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticle()
        {
            return await _dbContext.NewsArticles.ToListAsync();
        }

        public Task<IEnumerable<NewsArticle>> GetAllArticles()
        {
            throw new NotImplementedException();
        }


        public async Task<NewsArticle> GetNewsArticleById(string id)
        {
            return await _dbContext.NewsArticles.FindAsync(id);
        }


        public async Task<NewsArticle> UpdateNewsArticle(NewsArticle news)
        {
            return null;
        }

        //public async Task<short?> GetMaxNewsArticleIdAsync()
        //{
        //    return await _dbContext.NewsArticles
        //        .Where(n => short.TryParse(n.NewsArticleId, out _))  // Filter IDs that can be parsed to short
        //        .Select(n => (short?)Convert.ToInt16(n.NewsArticleId)) // Convert to short
        //        .MaxAsync(); // Find max value
        //}


        public async Task<List<NewsArticle>> GetAllNewsByDate(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
        .Include(n => n.CreatedBy)
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetNewsByCategoryAndDate(int categoryId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
        .Include(n => n.CreatedBy)
                .Where(n => n.CategoryId == categoryId && n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetNewsByStatusAndDate(bool status, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
        .Include(n => n.CreatedBy)
                .Where(n => n.NewsStatus == status && n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetNewsByCreateByAndDate(int userId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
        .Include(n => n.CreatedBy)
                .Where(n => n.CreatedById == userId && n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .ToListAsync();
        }

    }
}
