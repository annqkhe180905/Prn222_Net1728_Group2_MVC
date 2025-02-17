using BLL.DTOs;

namespace Net1728Group2MVC.Models
{
    public class CategoryModal
    {
        public short CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string CategoryDesciption { get; set; } = null!;

        public short? ParentCategoryId { get; set; }

        public bool? IsActive { get; set; }

        public IEnumerable<CategoryVM>? Categories { get; set; }
    }
}
