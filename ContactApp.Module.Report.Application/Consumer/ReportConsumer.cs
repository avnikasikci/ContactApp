using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.Report.Application.Job;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Consumer
{

    public class ReportConsumer : IConsumer<CustomerReport>
    {
        private readonly ICreateReportJobService _ReportService;

        public ReportConsumer(ICreateReportJobService ReportService)
        {
            _ReportService = ReportService;
        }

        public async Task Consume(ConsumeContext<CustomerReport> context)
        {
            _ReportService.StartJob(context);
        }

    }
}
