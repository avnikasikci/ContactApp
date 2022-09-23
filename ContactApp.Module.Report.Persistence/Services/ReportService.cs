using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Repository;
using ContactApp.Module.Report.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Services
{

    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(
            IReportRepository ReportRepository


            )
        {
            _reportRepository = ReportRepository;

        }

        public IQueryable<EntityReport> GetAll()
        {
            return _reportRepository.GetAll();
        }
        public EntityReport Add(EntityReport entityPerson)
        {

            return _reportRepository.Add(entityPerson);
        }
        public EntityReport Update(EntityReport entityPerson)
        {

            return _reportRepository.Update(entityPerson);
        }
        public EntityReport Save(EntityReport entityPerson)
        {

            return _reportRepository.Save(entityPerson);
        }

        public EntityReport SelectById(int id)
        {
            return _reportRepository.SelectById(id);
        }
    }
}
