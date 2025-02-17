using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public StaffController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult News()
        {
            return View();
        }
        public async Task<IActionResult> Category()
        {
            var listCate = await _categoryService.GetAll();
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
