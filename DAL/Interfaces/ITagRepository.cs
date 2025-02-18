using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITagRepository
    {
        public IEnumerable<Tag> GetAllTags();
        public Task<Tag> GetTagByIdAsync(int id);
        public void CreateTag(Tag tag);
        public void UpdateTag(Tag tag);
        public void DeleteTag(int id);

        public IEnumerable<Tag> Search(string keyword);
    }
}
