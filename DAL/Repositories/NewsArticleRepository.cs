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

        public NewsArticleRepository (FunewsManagementContext dbContext)
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
                .Where(n => n.NewsStatus == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticles()
        {
            return await _dbContext.NewsArticles.ToListAsync();
        }

        public async Task<NewsArticle> GetNewsArticleById(string id)
        {
            return await _dbContext.NewsArticles.FindAsync(id);
        }

        public async Task<NewsArticle> UpdateNewsArticle(NewsArticle news)
        {
            _dbContext.NewsArticles.Update(news);
            await _dbContext.SaveChangesAsync();
            return news;
        }
    }
}
