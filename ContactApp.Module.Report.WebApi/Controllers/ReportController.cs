
using ContactApp.Module.Person.Application.Features.Report.Queries;
using ContactApp.Module.Report.Application.Features.Report.Command;
using ContactApp.Module.Report.Application.Features.Report.Dtos;
using ContactApp.Module.Report.Application.Features.Report.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListReportQuery getListPersonQuery = new() { };
            List<ReportDto> result = await this.Mediator.Send(getListPersonQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateReportCommand createReportCommand)
        {
            CreatedReportDto result = await Mediator.Send(createReportCommand);
            return Created("", result);
        }
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
