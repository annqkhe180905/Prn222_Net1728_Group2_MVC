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

        public Task<bool> CreateTagAsync(TagVM tagVM)
        {
            var newsTag = _mapper.Map<Tag>(tagVM);
            return _tagRepository.CreateTagAsync(newsTag);
            
        }
        public async Task<bool> DeleteTagAsync(int tagId)
        {
            var tag = await _tagRepository.GetTagByIdAsync(tagId);
            if(tag == null) 
                return false;
            await _tagRepository.DeleteTagAsync(tagId);
            return true;
        }

        public async Task<IEnumerable<TagVM>> GetAllTagsAsync()
        {
            var list = await _tagRepository.GetAllTagsAsync();
            return _mapper.Map<List<TagVM>>(list);  
        }

        public TagVM GetTagById(int id)
        {
            var newsTag = _tagRepository.GetTagByIdAsync(id);
            return _mapper.Map<TagVM>(newsTag);
        }


        public async Task<bool> UpdateTagAsync(TagVM tagVM)
        {
            var newsTag = _mapper.Map<Tag>(tagVM);
            return await _tagRepository.UpdateTagAsync(newsTag);
        }
    }
}
