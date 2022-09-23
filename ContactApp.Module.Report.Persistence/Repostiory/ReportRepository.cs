using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Repository;
using ContactApp.Module.Report.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Persistence.Repostiory
{
    public class ReportRepository : IReportRepository
    {
        private readonly PGDataReportContext _context;

        public ReportRepository(PGDataReportContext context)
        {
            _context = context;
        }
        public IQueryable<EntityReport> GetAll()
        {
            return _context.EntityReports.AsQueryable().Where(x => x.Active);
        }
        public EntityReport Add(EntityReport entityPerson)
        {

            var result = _context.EntityReports.Add(entityPerson);
            _context.SaveChanges();
            return result.Entity;
        }
        public EntityReport Update(EntityReport entityPerson)
        {

            var result = _context.EntityReports.Update(entityPerson);
            _context.SaveChanges();
            return result.Entity;
        }
        public EntityReport Save(EntityReport entityPerson)
        {
            if (entityPerson.Id > 0)
            {
                _context.EntityReports.Update(entityPerson);

            }
            else
            {
                _context.EntityReports.Add(entityPerson);
            }
            _context.SaveChanges();
            return entityPerson;
        }
        public void Delete(EntityReport entityPerson)
        {
            _context.EntityReports.Remove(entityPerson);
        }

        public EntityReport SelectById(int id)
        {
            var Entity = _context.EntityReports.FirstOrDefault(x => x.Id == id);
            return Entity;
        }
    }
}
