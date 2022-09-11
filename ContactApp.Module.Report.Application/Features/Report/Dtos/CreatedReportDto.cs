using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Features.Report.Dtos
{
    public class CreatedReportDto
    {
        public string ReportName { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ReportStatus { get; set; }

        public string Data { get; set; }

        public bool Active { get; set; }
    }
}
