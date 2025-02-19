using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagVM>> GetAllTagsAsync();
        TagVM GetTagById(int id);
        Task<bool> CreateTagAsync(TagVM tag);
        Task<bool> UpdateTagAsync(TagVM tag);
        Task<bool> DeleteTagAsync(int tagId);

    }
}
