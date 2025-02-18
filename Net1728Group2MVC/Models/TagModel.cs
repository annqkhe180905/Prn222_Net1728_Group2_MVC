using BLL.DTOs;
namespace Net1728Group2MVC.Models
{
    public class TagModel
    {
        public int TagId { get; set; }

        public string? TagName { get; set; }

        public string? Note { get; set; }

        public IEnumerable<TagVM>? Tag { get; set; }
    }
}
