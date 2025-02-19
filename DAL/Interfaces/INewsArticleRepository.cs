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
        Task<List<NewsArticle>> GetAllNewsByDate (DateTime startDate, DateTime endDate);
        Task<List<NewsArticle>> GetNewsByStatusAndDate (bool status, DateTime startDate, DateTime endDate);
        Task<List<NewsArticle>> GetNewsByCategoryAndDate (int categoryId, DateTime startDate, DateTime endDate);
        Task<List<NewsArticle>> GetNewsByCreateByAndDate (int userId,  DateTime startDate, DateTime endDate);
        Task<IEnumerable<NewsArticle>> SearchArticles(string search, int? categoryId, List<int>? tagIds, short? createdBy);
    }
}
