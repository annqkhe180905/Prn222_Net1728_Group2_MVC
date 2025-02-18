using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FunewsManagementContext _context;

        public TagRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public void CreateTag(Tag tag)
        {
            try
            {
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public void DeleteTag(int tagId)
        {
            try
            {
                
                var tag = _context.Tags.Find(tagId);
                if (tag != null)
                {
                    _context.Tags.Remove(tag);
                    _context.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException("Tag not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Tag> GetAllTags()
        {
            List<Tag> tagsList = new List<Tag>();
            try
            {
                tagsList = _context.Tags.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tagsList;
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public void UpdateTag(Tag tag)
        {
            try
            {
                var existingTag = _context.Tags.Find(tag.TagId);
                if (existingTag != null)
                {
                    existingTag.TagName = tag.TagName;
                    existingTag.Note = tag.Note;
                    _context.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException("Tag not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
