using ContactApp.Module.Report.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Services.Interfaces
{

    public interface IReportService
    {
        IQueryable<EntityReport> GetAll();
        EntityReport Save(EntityReport EntityReport);
        EntityReport SelectById(string objectId);
    }
}
