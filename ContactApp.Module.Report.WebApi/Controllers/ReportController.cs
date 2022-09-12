
using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.Person.Application.Features.Report.Queries;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Features.Report.Command;
using ContactApp.Module.Report.Application.Features.Report.Dtos;
using ContactApp.Module.Report.Application.Features.Report.Queries;
using ContactApp.Module.Report.Application.Job;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {

        private readonly ICreateReportJobService _CreateReportJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IExportService _ExportService;


        public ReportController(ICreateReportJobService ReportService, IExportService ExportService, IHostingEnvironment hostingEnvironment)
        {
            _CreateReportJobService = ReportService;
            _ExportService = ExportService;
            this._hostingEnvironment = hostingEnvironment;
        }
        //[HttpGet("export")]
        [HttpGet("Export{ObjectId}")]

        public async Task<IActionResult> Export(string ObjectId)
        {
            
            #region demo data
            //        string folder = _hostingEnvironment.ContentRootPath;
            //        string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            //        string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
            //        FileInfo file = new FileInfo(Path.Combine(folder, excelName));
            //        if (file.Exists)
            //        {
            //            file.Delete();
            //            file = new FileInfo(Path.Combine(folder, excelName));
            //        }

            //        // query data from database  
            //        await Task.Yield();

            //        // query data from database  
            //        await Task.Yield();
            //        var list = new List<dynamic>()
            //{
            //    new  { UserName = "catcher", Age = 18 },
            //    new  { UserName = "james", Age = 20 },
            //};

            //        using (var package = new ExcelPackage(file))
            //        {
            //            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            //            workSheet.Cells.LoadFromCollection(list, true);
            //            package.Save();
            //        }
            //        var resultUrl = downloadUrl;
            //        CustomerReport customerReport = new CustomerReport() { Data = new List<CustomerReportData>() };

            //        var SaveEntity = new EntityReport();
            //        SaveEntity.ReportName = customerReport.ReportName;
            //        SaveEntity.CreateTime = DateTime.Now;
            //        SaveEntity.UpdateTime = DateTime.Now;

            //        SaveEntity.DataJson = customerReport.DataJson;
            //        SaveEntity.Active = true;
            //        SaveEntity.Data = new List<EntityReportData>();
            //        foreach (var item in customerReport.Data)
            //        {
            //            var saveDataEntity = new EntityReportData();
            //            saveDataEntity.Location = item.Location;
            //            saveDataEntity.UserCount = item.UserCount;
            //            saveDataEntity.PhoneCount = item.PhoneCount;
            //            saveDataEntity.MailCount = item.MailCount;
            //            SaveEntity.Data.Add(saveDataEntity);
            //        }


            //        byte[] exportResult = _ExportService.ExportListToByteArray(SaveEntity.Data, new ExportDescriptor<EntityReportData>
            //        {
            //            Items = new List<ExportDescriptorItem<EntityReportData>>
            //                    {
            //                                                        new ExportDescriptorItem<EntityReportData>{ Title = "Location", Expression = c => c.Location },
            //                                                        new ExportDescriptorItem<EntityReportData>{ Title = "UserCount", Expression = c => c.UserCount },
            //                                                        new ExportDescriptorItem<EntityReportData>{ Title = "PhoneCount", Expression = c => c.PhoneCount },
            //                                                        new ExportDescriptorItem<EntityReportData>{ Title = "MailCount", Expression = c => c.MailCount },

            //                    }
            //        });



            //        var stream = new MemoryStream();

            //        using (var package = new ExcelPackage(stream))
            //        {
            //            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            //            workSheet.Cells.LoadFromCollection(list, true);
            //            package.Save();
            //        }
            //        stream.Position = 0;
            //        //string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //        //return File(stream, "application/octet-stream", excelName);  
            //        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            #endregion

            GetByIdReportQuery getByIdUserQuery = new() { ObjectId = ObjectId };

            ReportDto result = await Mediator.Send(getByIdUserQuery) ?? new ReportDto();
            result.Data = (result.Data != null) ? result.Data : new List<EntityReportData>();
          
            byte[] exportResult = _ExportService.ExportListToByteArray(result.Data, new ExportDescriptor<EntityReportData>
            {
                Items = new List<ExportDescriptorItem<EntityReportData>>
                        {
                                                            new ExportDescriptorItem<EntityReportData>{ Title = "Location", Expression = c => c.Location },
                                                            new ExportDescriptorItem<EntityReportData>{ Title = "UserCount", Expression = c => c.UserCount },
                                                            new ExportDescriptorItem<EntityReportData>{ Title = "PhoneCount", Expression = c => c.PhoneCount },
                                                            new ExportDescriptorItem<EntityReportData>{ Title = "MailCount", Expression = c => c.MailCount },

                        }
            });

            return File(exportResult, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportUser(" + DateTime.Now.ToShortDateString() + ").xlsx");

        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            //_CreateReportJobService.StartJob();
            ////new CreateReportJobService().StartJob();
            GetListReportQuery getListPersonQuery = new() { };
            List<ReportDto> result = await this.Mediator.Send(getListPersonQuery);
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] CreateReportCommand createReportCommand)
        //{
        //    CreatedReportDto result = await Mediator.Send(createReportCommand);
        //    return Created("", result);
        //}
        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        //{
        //    UpdateUserDto result = await Mediator.Send(updateUserCommand);
        //    return Created("", result);
        //}
        ////[HttpDelete]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteReportCommand deleteUserCommand)
        {
            string result = await Mediator.Send(deleteUserCommand);
            return Created("", result);
        }
        [HttpGet("{ObjectId}")]
        public async Task<IActionResult> GetById(string ObjectId)
        {
            GetByIdReportQuery getByIdUserQuery = new() { ObjectId = ObjectId };

            ReportDto result = await Mediator.Send(getByIdUserQuery);
            return Created("", result);
        }
    }
}
