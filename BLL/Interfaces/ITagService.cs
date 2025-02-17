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
        List<TagVM> GetAllTags();
        TagVM GetTagById(int id);
        void CreateTag(TagVM tag);
        void UpdateTag(TagVM tag);
        void DeleteTag(int tagId);
    }
}
