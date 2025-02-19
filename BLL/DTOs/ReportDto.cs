using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ReportDto
    {
        public int TotalNews {  get; set; }
        public Dictionary<string, int> NewsByStatus { get; set; } = new();
        public Dictionary<string, int> NewsByCategory { get; set; } = new();
        public Dictionary<string, (string Email, int Count)> NewsByCreator { get; set; } = new();  // 🟢 Thêm email người tạo
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
