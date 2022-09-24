using ContactApp.Module.Report.Application.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Persistence.Context
{
    public partial class PGDataReportContext : DbContext
    {
        public DbSet<EntityReport> EntityReports { get; set; }     


        public PGDataReportContext(DbContextOptions<PGDataReportContext> options)
      : base(options)
        {
            //Configuration.LazyLoadingEnabled = false;
        }
    }
}
