using BLL.DTOs;
using DAL.Entities;

namespace Net1728Group2MVC.Models
{
    public class NewsModel
    {
        public IEnumerable<CategoryVM>? Categories { get; set; }
        public IEnumerable<TagVM>? Tags { get; set; }
        public IEnumerable<NewsArticleVM> NewsArticles { get; set; }
    }
}
