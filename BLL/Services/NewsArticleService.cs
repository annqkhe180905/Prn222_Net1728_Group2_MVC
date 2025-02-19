using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BLL.DTOs;

namespace BLL.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly IMapper _mapper;

        public NewsArticleService(INewsArticleRepository newsArticleRepository, IMapper mapper)
        {
            _newsArticleRepository = newsArticleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsArticle>> SearchArticles(string search, int? categoryId, List<int>? tagIds, string? createdBy)
        {
            return await _newsArticleRepository.SearchArticles(search, categoryId, tagIds, createdBy);
        }

        public async Task<NewsArticle> CreateNewsArticle(NewsArticleVM model)
        {
            var newsArticle = _mapper.Map<NewsArticle>(model);
            model.CreatedDate = DateTime.Now;
            await _newsArticleRepository.CreateNewsArticle(newsArticle);

            return newsArticle;
        }

        public async Task<bool> DeleteNewsArticle(string id)
        {
            var deletedArticle = await _newsArticleRepository.GetNewsArticleById(id);
            if (deletedArticle == null)
            {
                return false;
            }
            return await _newsArticleRepository.DeleteNewsArticle(deletedArticle);
        }

        public async Task<IEnumerable<NewsArticle>> GetAllActiveArticles()
        {
            return await _newsArticleRepository.GetAllActiveArticles();
        }

        public async Task<NewsArticle> GetNewsArticleById(string id)
        {
            return await _newsArticleRepository.GetNewsArticleById(id);
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticle()
        {
            return await _newsArticleRepository.GetAllArticles();
        }

        public async Task<NewsArticle> UpdateNewsArticle(NewsArticleVM news)
        {
            var updateArticle = await _newsArticleRepository.GetNewsArticleById(news.NewsArticleId);
            if (updateArticle == null)
            {
                return null;
            }
            updateArticle = _mapper.Map(news, updateArticle);
            updateArticle.ModifiedDate = DateTime.Now;
            await _newsArticleRepository.UpdateNewsArticle(updateArticle);
            return updateArticle;
        }

    }
}
