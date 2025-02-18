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
        List<Tag> GetAllTags();
        Task<Tag> GetTagByIdAsync(int id);
        void CreateTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int tagId);
    }
}
