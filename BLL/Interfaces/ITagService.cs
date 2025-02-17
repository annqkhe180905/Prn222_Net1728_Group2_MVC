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
        List<Tag> GetTags();
        void CreateTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int tagId);
    }
}
