using AutoMapper;
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

    public partial class DeleteReportCommand : IRequest<string>
    {
        public string id { get; set; }


    }
 
}
