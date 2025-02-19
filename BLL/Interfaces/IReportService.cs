using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        Task<ReportDto> GenerateReport (DateTime startDate, DateTime endDate);
    }
}
