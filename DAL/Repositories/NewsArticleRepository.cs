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
            _dbContext.NewsArticles.Add(news);
            await _dbContext.SaveChangesAsync();
            return news;
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
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .Where(n => n.NewsStatus == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticles()
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticle(string? search)
        {
            var query = _dbContext.NewsArticles.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.NewsTitle.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<NewsArticle> GetNewsArticleById(string id)
        {
            return await _dbContext.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);

        }


        public async Task<NewsArticle> UpdateNewsArticle(NewsArticle news)
        {
            _dbContext.NewsArticles.Update(news);
            await _dbContext.SaveChangesAsync();
            return news;
        }

        public async Task<IEnumerable<NewsArticle>> SearchArticles(string search, int? categoryId, List<int>? tagIds, short? createdBy)
        {
            var query = _dbContext.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.NewsTitle.Contains(search) || n.NewsContent.Contains(search));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(n => n.CategoryId == categoryId);
            }
            if (tagIds != null && tagIds.Any())
            {
                query = query.Where(n => n.Tags.Any(t => tagIds.Contains(t.TagId)));
            }
            if (createdBy.HasValue)
            {
                query = query.Where(n => n.CreatedById == createdBy);
            }
            return await query.ToListAsync();
        }

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
