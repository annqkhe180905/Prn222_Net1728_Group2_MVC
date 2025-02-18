using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddAsync(CategoryVM incomingCategory);
        Task<bool> UpdateAsync(CategoryVM editCategory);
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<bool> RemoveAsync(int id);
    }
}
