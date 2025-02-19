namespace Net1728Group2MVC.Models
{
    public class ReportModel
    {
        public string ReportPeriod { get; set; }
        public int TotalNews { get; set; }
        public List<StatusReport> StatusReports { get; set; } = new();
        public List<CategoryReport> CategoryReports { get; set; } = new();
        public List<CreatorReport> CreatorReports { get; set; } = new();
    }

    public class StatusReport
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }

    public class CreatorReport
    {
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; } 
        public int Count { get; set; }
    }
}
