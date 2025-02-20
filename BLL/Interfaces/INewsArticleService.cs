using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticle>> GetAllActiveArticles();
        Task<IEnumerable<NewsArticle>> GetAllArticles();
        Task<IEnumerable<NewsArticleVM>> GetAllArticle(string? search);
        Task<NewsArticle> GetNewsArticleById(string id);
        Task<NewsArticle> CreateNewsArticle(NewsArticleVM model);
        Task<NewsArticle> UpdateNewsArticle(NewsArticleVM news);
        Task<bool> DeleteNewsArticle(string id);
        Task<IEnumerable<NewsArticle>> SearchArticles (string search, int? categoryId, List<int>? tagIds, short? createdBy);
    }
}