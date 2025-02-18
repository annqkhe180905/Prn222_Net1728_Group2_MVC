using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public StaffController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult News()
        {
            return View();
        }
        public async Task<IActionResult> Category()
        {
            var categoryModal = new CategoryModal()
            {
                Categories = await _categoryService.GetAllAsync()
            };
            return View(categoryModal);
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
