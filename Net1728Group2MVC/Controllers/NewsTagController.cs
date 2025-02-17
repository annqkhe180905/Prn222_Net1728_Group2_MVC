using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Net1728Group2MVC.Controllers
{
    public class NewsTagController : Controller
    {
        private readonly ITagService _tagService;
        public NewsTagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IList<Tag> Tag { get; set; } = default!;
    }
}
