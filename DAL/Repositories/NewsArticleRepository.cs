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
            throw new NotImplementedException();
        }

        public async Task<NewsArticle> DeleteNewsArticle(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllActiveArticles()
        {
            throw new NotImplementedException();
        }

        public async Task<NewsArticle> GetNewsArticleById(string id)
        {
            try
            {
                var result = _dbContext.NewsArticles.FirstOrDefault(x => x.NewsArticleId == id);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NewsArticle> UpdateNewsArticle(NewsArticle news)
        {
            throw new NotImplementedException();
        }

        //public async Task<short?> GetMaxNewsArticleIdAsync()
        //{
        //    return await _dbContext.NewsArticles
        //        .Where(n => short.TryParse(n.NewsArticleId, out _))  // Filter IDs that can be parsed to short
        //        .Select(n => (short?)Convert.ToInt16(n.NewsArticleId)) // Convert to short
        //        .MaxAsync(); // Find max value
        //}
    }
}
