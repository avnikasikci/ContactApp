using AutoMapper;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Features.Report.Command;
using ContactApp.Module.Report.Application.Features.Report.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Features.Report.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<EntityReport,ReportDto>().ReverseMap();
            CreateMap<EntityReport, CreatedReportDto>().ReverseMap();            
            CreateMap<EntityReport, CreateReportCommand>().ReverseMap();
            
            
        }
    }

}
