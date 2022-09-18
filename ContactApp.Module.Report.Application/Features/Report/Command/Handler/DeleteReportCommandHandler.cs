﻿using AutoMapper;
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
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, string>
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public DeleteReportCommandHandler(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var Entity = _reportService.SelectById(request.ObjectId);
            if (Entity != null)
            {
                //Entity.Active = false;
                Entity.setActive(false);
                _reportService.Save(Entity);
                return Entity.ObjectId;

            }
            else
            {
                return "Not Found User";
            }

        }
    }
}