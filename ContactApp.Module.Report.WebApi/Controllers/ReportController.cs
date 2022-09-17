
using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Core.Application.SharedModels;
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
        private readonly IExportService _ExportService;


        public ReportController(ICreateReportJobService ReportService, IExportService ExportService)
        {
            _CreateReportJobService = ReportService;
            _ExportService = ExportService;
        }
        //[HttpGet("export")]
        [HttpGet("Export{ObjectId}")]

        public async Task<IActionResult> Export(string ObjectId)
        {

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
