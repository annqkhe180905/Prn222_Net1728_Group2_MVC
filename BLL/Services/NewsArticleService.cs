using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
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
        
        public NewsArticleService (INewsArticleRepository newsArticleRepository, IMapper mapper)
        {
            newsArticleRepository = _newsArticleRepository;
            mapper = _mapper;
        }

        public async Task<NewsArticle> CreateNewsArticle(NewsArticleRequestModel model)
        {
            var newsArticle = _mapper.Map<NewsArticle>(model);

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

        public async Task<IEnumerable<NewsArticle>> GetAllActiveAsync()
        {
            return await _newsArticleRepository.GetAllActiveArticles();
        }

        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            return await _newsArticleRepository.GetNewsArticleById(id);
        }

        public async Task<NewsArticleRequestModel> UpdateNewsArticle(NewsArticleRequestModel news)
        {
            return null;
        }
    }
}
