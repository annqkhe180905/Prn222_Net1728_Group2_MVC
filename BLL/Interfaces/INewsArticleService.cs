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
        Task<IEnumerable<NewsArticle>> GetAllActiveAsync();
        Task<NewsArticle> GetNewsArticleByIdAsync(string id);
        Task<NewsArticle> CreateNewsArticle(NewsArticleRequestModel model);
        Task<NewsArticleRequestModel> UpdateNewsArticle(NewsArticleRequestModel news);
        Task<bool> DeleteNewsArticle(string id);
    }
}