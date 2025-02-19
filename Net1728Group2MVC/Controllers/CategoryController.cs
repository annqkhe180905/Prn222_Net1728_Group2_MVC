using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;
using System.Text.Json;

namespace Net1728Group2MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM incomingCategory)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            bool succeed = await _categoryService.AddAsync(incomingCategory);
            return succeed ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM editCategory)
        {
            bool succeed = await _categoryService.UpdateAsync(editCategory);
            return succeed ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                bool succeed = await _categoryService.RemoveAsync(id);
                return succeed ? Ok() : BadRequest("xóa category thất bại");
            }
            catch (Exception) 
            {
                return StatusCode(500, "Có lỗi xảy ra khi xóa category");
            }
        }
    }
}
