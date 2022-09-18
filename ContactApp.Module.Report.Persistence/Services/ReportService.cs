using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Report.Application.Domain;
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
        private readonly IMongoDbRepository<EntityReport> _reportRepository;
        public ReportService(
            IMongoDbRepository<EntityReport> ReportRepository


            )
        {
            _reportRepository = ReportRepository;

        }

        public IQueryable<EntityReport> GetAll()
        {
            return _reportRepository.All.Where(x => x.Active);
        }
        public EntityReport Save(EntityReport entityPerson)
        {
            if (!string.IsNullOrEmpty(entityPerson.ObjectId))
            {
                _reportRepository.UpdateAsync(entityPerson, x => x.ObjectId == entityPerson.ObjectId);
            }
            else
            {
                _reportRepository.AddAsync(entity: entityPerson);
            }
            return entityPerson;
        }

        public EntityReport SelectById(string objectId)
        {
            var Entity = _reportRepository.All.Where(x => x.ObjectId == objectId).FirstOrDefault();
            return Entity;
        }
    }
}
