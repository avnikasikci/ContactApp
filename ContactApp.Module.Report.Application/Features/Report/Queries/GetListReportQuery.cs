﻿using AutoMapper;
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

namespace ContactApp.Module.Report.Application.Features.Report.Queries
{

    public class GetListReportQuery : IRequest<List<ReportDto>>
    {
        //public PageRequest PageRequest { get; set; }
        public class GetListUserQueryHandler : IRequestHandler<GetListReportQuery, List<ReportDto>>
        {
            private readonly IReportService _ReportSerivce;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IReportService reportService, IMapper mapper)
            {
                _ReportSerivce = reportService;
                _mapper = mapper;
            }

            public async Task<List<ReportDto>> Handle(GetListReportQuery request, CancellationToken cancellationToken)
            {
                List<EntityReport> personList = _ReportSerivce.GetAll().ToList();
                var mappedPersonListModel = (from m in personList
                                             select new ReportDto
                                             {
                                                 ObjectId = m.ObjectId,
                                                 Active = m.Active,
                                                 CreateTime = m.CreateTime,
                                                 UpdateTime = m.UpdateTime,
                                                 ReportStatus = m.ReportStatus,
                                                 FilePath = m.FilePath,
                                             }).ToList();

                return mappedPersonListModel;
            }
        }
    }

}
