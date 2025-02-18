using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CategoryVM incomingCategory)
        {
            var newCategory = new Category
            {
                CategoryName = incomingCategory.CategoryName,
                CategoryDesciption = incomingCategory.CategoryDesciption,
                IsActive = true,
            };
            return await _categoryRepository.AddAsync(newCategory);
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var list = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryVM>>(list);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var category = await _categoryRepository.FindAsync(id);

            if (category == null)
                return false;

            await _categoryRepository.RemoveAsync(category);
            return true;
        }

        public async Task<bool> UpdateAsync(CategoryVM editCategory)
        {
            var existCategory = await _categoryRepository.FindAsync(editCategory.CategoryId);

            if (existCategory == null)
                return false;

            if (!string.IsNullOrEmpty(editCategory.CategoryName))
            {
                existCategory.CategoryName = editCategory.CategoryName;
            }
            if (!string.IsNullOrEmpty(editCategory.CategoryDesciption))
            {
                existCategory.CategoryDesciption = editCategory.CategoryDesciption;
            }
            if (existCategory.IsActive != editCategory.IsActive)
            {
                existCategory.IsActive = (bool)editCategory.IsActive;
            }
            return await _categoryRepository.UpdateAsync(existCategory);
        }
    }
}
