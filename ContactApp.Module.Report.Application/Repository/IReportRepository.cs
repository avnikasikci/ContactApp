using ContactApp.Module.Report.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Repository
{
    public interface IReportRepository
    {
        IQueryable<EntityReport> GetAll();
        EntityReport Save(EntityReport EntityReport);
        EntityReport Add(EntityReport EntityReport);
        EntityReport Update(EntityReport EntityReport);
        EntityReport SelectById(int id);
        void Delete(EntityReport entityPerson);
    }
}
