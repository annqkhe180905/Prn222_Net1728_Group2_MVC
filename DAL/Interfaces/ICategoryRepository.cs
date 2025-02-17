using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> AddAsync(Category newCategory);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category>? GetLastItem();

        Task<Category>? FindAsync(int id);
        Task<bool> RemoveAsync(Category category);
        Task<bool> UpdateAsync(Category existCategory);
    }
}
