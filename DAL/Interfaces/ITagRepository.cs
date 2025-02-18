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
        public Task<IEnumerable<Tag>> GetAllTagsAsync(string? search);
        public Task<Tag> GetTagByIdAsync(int id);
        public Task<bool> CreateTagAsync(Tag tag);
        public Task<bool> UpdateTagAsync(Tag tag);
        public Task<bool> DeleteTagAsync(int id);

    }
}
