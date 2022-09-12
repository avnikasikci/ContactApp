using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.Person.Application.Features.User.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly IBus _busService;
        public ReportController(IBus busService)
        {
            _busService = busService;
        }
        //[HttpPost]
        //public async Task<string> CreateReport(CustomerReport Report)
        //{
        //    if (Report != null)
        //    {
        //        Report.AddedOnDate = DateTime.Now;
        //        Uri uri = new Uri("rabbitmq://localhost/reportQueue");
        //        var endPoint = await _busService.GetSendEndpoint(uri);
        //        await endPoint.Send(Report);
        //        return "Ready Created Report";
        //    }
        //    return "Report Not Found";
        //}
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GetCustomerReportQuery createUserCommand)
        {
            CustomerReport Report = await Mediator.Send(createUserCommand);
            var msg = "";
            if (Report != null)
            {
                Report.AddedOnDate = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/reportQueue");
                var endPoint = await _busService.GetSendEndpoint(uri);
                await endPoint.Send(Report);
                msg= "Ready Created Report";
            }
            msg = "Report Not Found";
            return Created("", Report);
        }
    }
}
