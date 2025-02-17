using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private FunewsManagementContext _context;
        public CategoryRepository(FunewsManagementContext _context)
        {
            this._context = _context;
        }

        public async Task<bool> AddAsync(Category newCategory)
        {
            try
            {
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category>? GetLastItem() => await _context.Categories?.OrderByDescending(c => c.CategoryId).FirstOrDefaultAsync();

        public async Task<bool> RemoveAsync(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Category>? FindAsync(int id) => await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        public async Task<bool> UpdateAsync(Category existCategory)
        {
            try
            {
                _context.Categories.Update(existCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
