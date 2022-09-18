using AutoMapper;
using ContactApp.Module.Report.Application.Features.Report.Dtos;
using ContactApp.Module.Report.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Features.Report.Queries.Handler
{
    public class GetByIdReportQueryHandler : IRequestHandler<GetByIdReportQuery, ReportDto>
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public GetByIdReportQueryHandler(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<ReportDto> Handle(GetByIdReportQuery request, CancellationToken cancellationToken)
        {
            var Entity = _reportService.SelectById(request.ObjectId);
            ReportDto dto = _mapper.Map<ReportDto>(Entity);

            return dto;
        }
    }
}
