using AutoMapper;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Features.Report.Dtos;
using ContactApp.Module.Report.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Features.Report.Command
{

    public partial class CreateReportCommand : IRequest<CreatedReportDto>
    {
        public string ReportName { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

   
    }

   

}
