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

        private static int _lockFlag = 0; // 0 - free // Bir işlem devam ederken gelen diğer işlemler iptal edilsin sırada beklemesin diye. Flag kontrolü

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


        public void StartJob(/*JobParam Param*/  ConsumeContext<CustomerReport> context = null)
        {
            var cancellationToken = new CancellationTokenSource().Token;
            Task.Run(async () =>
            {
                await StartProcessing(cancellationToken/*, Param */, context);
            }, cancellationToken);


        }

        private async Task StartProcessing(CancellationToken cancellationToken = default(CancellationToken), ConsumeContext<CustomerReport> context = null)
        {


            //if (Interlocked.CompareExchange(ref _lockFlag, 1, 0) == 0) // Lock olmuş ise çağrı sırada beklemesin iptal etsin işlemi.
            //{
            //    //fonsiyon aynı anda bir kere çalışın diye 
            //    lock (_SyncRoot)
            //    {
            CustomerReport customerReport = context?.Message ?? new CustomerReport() { Data = new List<CustomerReportData>() };

            var SaveEntity = new EntityReport();
            SaveEntity.ReportName = customerReport.ReportName;
            SaveEntity.CreateTime = DateTime.Now;
            SaveEntity.UpdateTime = DateTime.Now;
            SaveEntity.ReportStatus = (int)EnumCollection.ReportStatus.Wait; //Excell file then done this make done
            SaveEntity.DataJson = customerReport.DataJson;
            SaveEntity.Active = true;
            
            SaveEntity.Data = new List<EntityReportData>();
            foreach (var item in customerReport.Data)
            {
                var saveDataEntity = new EntityReportData();
                saveDataEntity.Location = item.Location;
                saveDataEntity.UserCount = item.UserCount;
                saveDataEntity.PhoneCount = item.PhoneCount;
                saveDataEntity.MailCount = item.MailCount;
                SaveEntity.Data.Add(saveDataEntity);
            }

            //byte[] exportResult = _ExportService.ExportListToByteArray(SaveEntity.Data, new ExportDescriptor<EntityReportData>
            //{
            //    Items = new List<ExportDescriptorItem<EntityReportData>>
            //            {
            //                                                new ExportDescriptorItem<EntityReportData>{ Title = "Location", Expression = c => c.Location },
            //                                                new ExportDescriptorItem<EntityReportData>{ Title = "UserCount", Expression = c => c.UserCount },
            //                                                new ExportDescriptorItem<EntityReportData>{ Title = "PhoneCount", Expression = c => c.PhoneCount },
            //                                                new ExportDescriptorItem<EntityReportData>{ Title = "MailCount", Expression = c => c.MailCount },

            //            }
            //});
            //SaveEntity.FileByte = exportResult;

            SaveEntity= _ReportService.Save(SaveEntity);
            //SaveEntity.FilePath = "https://localhost:44397/api/Report/"+SaveEntity.ObjectId;
            SaveEntity.FilePath = "https://localhost:44397/api/Report/Export"+SaveEntity.ObjectId;
            _ReportService.Save(SaveEntity);


            //    }

            //}
        }
    }
    public interface ICreateReportJobService
    {
        void StartJob(ConsumeContext<CustomerReport> context = null);
    }

}
