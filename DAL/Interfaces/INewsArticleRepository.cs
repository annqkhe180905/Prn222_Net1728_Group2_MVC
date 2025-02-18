using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetAllActiveArticles();
        Task<IEnumerable<NewsArticle>> GetAllArticles();
        Task<NewsArticle> GetNewsArticleById(string id);
        Task <NewsArticle> CreateNewsArticle(NewsArticle news);
        Task <NewsArticle> UpdateNewsArticle(NewsArticle news);
        Task <bool> DeleteNewsArticle(NewsArticle news);
    }
}
