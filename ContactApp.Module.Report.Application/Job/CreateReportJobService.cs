using ContactApp.Core.Application.Core;
using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Enums;
using ContactApp.Module.Report.Application.Services.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Job
{

    public class CreateReportJobService : ICreateReportJobService
    {
        private static object _SyncRoot = new object();

        private static int _lockFlag = 0; // 0 - free // While a transaction is in progress, other incoming transactions are canceled so that it does not wait in the queue. Flag control

        public CreateReportJobService()
        {
        }
        private readonly IReportService _ReportService;
        private readonly IExportService _ExportService;

        public CreateReportJobService(IReportService ReportService, IExportService ExportService)
        {
            _ReportService = ReportService;
            _ExportService = ExportService;
        }


        public void StartJob(ConsumeContext<CustomerReport> context = null)
        {
            this.StartProcessing(context);
        }

        private void StartProcessing(ConsumeContext<CustomerReport> context = null)
        {
            if (context == null || context.Message == null)
            {
                return;
            }
            CustomerReport customerReport = context?.Message ?? new CustomerReport() { Data = new List<CustomerReportData>() };

            var SaveEntity = new EntityReport(0,customerReport.ReportName,DateTime.Now,DateTime.Now, (int)EnumCollection.ReportStatus.Wait,customerReport.DataJson,"", new(),true);
            foreach (var item in customerReport.Data)
            {
                var saveDataEntity = new EntityReportData();
                saveDataEntity.Location = item.Location;
                saveDataEntity.UserCount = item.UserCount;
                saveDataEntity.PhoneCount = item.PhoneCount;
                saveDataEntity.MailCount = item.MailCount;
                SaveEntity.Data.Add(saveDataEntity);
            }


            SaveEntity = _ReportService.Save(SaveEntity);
            SaveEntity.setFilePath("https://localhost:44397/api/Report/Export" + SaveEntity.Id);
            SaveEntity.setReportStatus((int)EnumCollection.ReportStatus.Done);
            _ReportService.Save(SaveEntity);
        }
    }
    public interface ICreateReportJobService
    {
        void StartJob(ConsumeContext<CustomerReport> context = null);
    }

}
