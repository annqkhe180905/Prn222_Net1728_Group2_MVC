using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<bool> CreateTagAsync(Tag tag)
        {
            try
            {
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

            
        }

        public async Task<bool> DeleteTagAsync(int tagId)
        {
            try
            {
                
                var tag = await _context.Tags.FindAsync(tagId);
                if (tag != null)
                {
                    _context.Tags.Remove(tag);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new KeyNotFoundException("Tag not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }


        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagId == id);
        }

        public IEnumerable<Tag> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Tag>();

            return _context.Tags.Where(t => t.TagName.Contains(keyword)).ToList();
        }

        public async Task<bool> UpdateTagAsync(Tag tag)
        {
            try
            {
                var existingTag = await _context.Tags.FindAsync(tag.TagId);
                if (existingTag != null)
                {
                    existingTag.TagName = tag.TagName;
                    existingTag.Note = tag.Note;
                    await _context.SaveChangesAsync();
                    return true;
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

        public async Task<IEnumerable<Tag>> GetAllTagsAsync(string? search)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.TagName.Contains(search));
            }

            return await query.ToListAsync();
        }
    }
}
