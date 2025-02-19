using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IReportService _reportService;

        public AdminController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Account()
        {
            ViewBag.Header = "Account Management";
            return View();
        }

        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Header = "Report Management";

            if (!startDate.HasValue || !endDate.HasValue)
            {
                startDate = DateTime.Today.AddDays(-30);
                endDate = DateTime.Today;
            }

            var reportDTO = await _reportService.GenerateReport(startDate.Value, endDate.Value);

            var viewModel = new ReportModel
            {
                ReportPeriod = $"{reportDTO.StartDate:dd/MM/yyyy} - {reportDTO.EndDate:dd/MM/yyyy}",
                TotalNews = reportDTO.TotalNews,
                StatusReports = reportDTO.NewsByStatus.Select(s => new StatusReport
                {
                    Status = s.Key,
                    Count = s.Value
                }).ToList(),
                CategoryReports = reportDTO.NewsByCategory.Select(c => new CategoryReport
                {
                    CategoryName = c.Key,
                    Count = c.Value
                }).ToList(),
                CreatorReports = reportDTO.NewsByCreator.Select(cr => new CreatorReport
                {
                    CreatorName = cr.Key,
                    CreatorEmail = cr.Value.Email,
                    Count = cr.Value.Count   
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
