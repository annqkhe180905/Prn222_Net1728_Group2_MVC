using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using BLL.ViewModels.Requests;
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

        public async Task<NewsArticleRequestModel> CreateNewsArticle(NewsArticleRequestModel newsModel)
        {
            var newsArticle = _mapper.Map<NewsArticle>(newsModel);
            List<int> tagIds = newsModel.TagIds ?? new List<int>();

            await _newsArticleRepository.CreateNewsArticle(newsArticle, tagIds);
        }

        public async Task<string> DeleteNewsArticle(string id)
        {
        }

        public async Task<IEnumerable<NewsArticle>> GetAllActiveAsync()
        {
        }

        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
        }

        public async Task<NewsArticleRequestModel> UpdateNewsArticle(NewsArticleRequestModel news)
        {
        }
    }
}
