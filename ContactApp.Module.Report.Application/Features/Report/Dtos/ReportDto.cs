using ContactApp.Module.Report.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Features.Report.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string ReportName { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ReportStatus { get; set; }
        public string DataJson { get; set; }
        public string FilePath { get; set; }
        public List<EntityReportData> Data { get; set; }
        public bool Active { get; set; }
    }
}
