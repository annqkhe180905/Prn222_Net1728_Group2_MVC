using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public void CreateTag(TagVM tagVM)
        {
            var newsTag = _mapper.Map<Tag>(tagVM);
            _tagRepository.CreateTag(newsTag);
        }
        public void DeleteTag(int tagId)
        {
            _tagRepository.DeleteTag(tagId);
        }

        public List<TagVM> GetAllTags()
        {
            var newsTags = _tagRepository.GetAllTags();
            return _mapper.Map<List<TagVM>>(newsTags);  
        }

        public TagVM GetTagById(int id)
        {
            return _mapper.Map<TagVM>(id);
        }

        public void UpdateTag(TagVM tagVM)
        {
            var newsTag = _mapper.Map<Tag>(tagVM);
            _tagRepository.UpdateTag(newsTag);
        }
    }
}
