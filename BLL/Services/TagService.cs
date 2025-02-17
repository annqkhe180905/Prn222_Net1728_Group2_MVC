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

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public void CreateTag(Tag tag) => _tagRepository.CreateTag(tag);
        public void DeleteTag(int tagId) => _tagRepository.DeleteTag(tagId);

        public List<Tag> GetTags() => _tagRepository.GetTags();

        public void UpdateTag(Tag tag) => _tagRepository.UpdateTag(tag);
    }
}
