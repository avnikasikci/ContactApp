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

namespace ContactApp.Module.Report.Application.Features.Report.Command.Handler
{
    public class CreateReportCommanddHandler : IRequestHandler<CreateReportCommand, CreatedReportDto>
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public CreateReportCommanddHandler(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<CreatedReportDto> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            EntityReport mappedReport = _mapper.Map<EntityReport>(request);
            //mappedReport.Active = true;
            mappedReport.setActive(true);
            EntityReport createdReport = _reportService.Save(mappedReport);
            CreatedReportDto createdPersonDto = _mapper.Map<CreatedReportDto>(createdReport);

            return createdPersonDto;

        }
    }
}
