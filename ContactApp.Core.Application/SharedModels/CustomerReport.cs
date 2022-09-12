using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.SharedModels
{
    public class CustomerReport
    {
        public string ReportName { get; set; }
        public string DataJson { get; set; }
        public List<CustomerReportData> Data { get; set; }

        public DateTime AddedOnDate { get; set; }
    }
    public class CustomerReportData
    {
        public string Location { get; set; }
        public string UserCount { get; set; }
        public string PhoneCount { get; set; }
        public string MailCount { get; set; }
    }
}
