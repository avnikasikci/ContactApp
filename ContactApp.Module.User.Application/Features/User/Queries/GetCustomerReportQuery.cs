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
        //public PageRequest PageRequest { get; set; }
        public string ReportName { get; set; }


        public class GetCustomerReportQueryHandler : IRequestHandler<GetCustomerReportQuery, CustomerReport>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

            public GetCustomerReportQueryHandler(IUserService userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<CustomerReport> Handle(GetCustomerReportQuery request, CancellationToken cancellationToken)
            {
                var report = _userService.GetCustomerReport(request.ReportName,DateTime.Now);
                return report;
            }
        }
    }

}
