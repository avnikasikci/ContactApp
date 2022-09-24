using AutoMapper;
using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Features.User.Dtos;
using ContactApp.Module.User.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Features.User.Queries
{

    public class GetCustomerReportQuery : IRequest<CustomerReport>
    {
        public string ReportName { get; set; }
    }

}
