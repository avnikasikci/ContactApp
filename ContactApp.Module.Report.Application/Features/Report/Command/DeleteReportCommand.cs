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
        public string ObjectId { get; set; }

        public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, string>
        {
            private readonly IReportService _ReportService;
            private readonly IMapper _mapper;

            public DeleteReportCommandHandler(IReportService ReportService, IMapper mapper)
            {
                _ReportService = ReportService;
                _mapper = mapper;
            }

            public async Task<string> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
            {
                var Entity = _ReportService.SelectById(request.ObjectId);
                if (Entity != null)
                {
                    //Entity.Active = false;
                    Entity.setActive(false);
                    _ReportService.Save(Entity);
                    return Entity.ObjectId;

                }
                else
                {
                    return "Not Found User";
                }

            }
        }
    }

}
