using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NewsArticleService : INewsArticleRepository
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly ILogger
        
        public NewsArticleService (NewsArticleRepository newsArticleRepository)
        {
            newsArticleRepository = (NewsArticleRepository)_newsArticleRepository;
        }

        public Task<NewsArticle> CreateNewsArticle(NewsArticle news)
        {
            throw new NotImplementedException();
        }

        public Task<NewsArticle> DeleteNewsArticle(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<NewsArticle> UpdateNewsArticle(NewsArticle news)
        {
            throw new NotImplementedException();
        }
    }
}
