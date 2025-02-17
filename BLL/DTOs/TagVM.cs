using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TagVM
    {
        public int TagId { get; set; }

        public string? TagName { get; set; }

        public string? Note { get; set; }
    }
}
