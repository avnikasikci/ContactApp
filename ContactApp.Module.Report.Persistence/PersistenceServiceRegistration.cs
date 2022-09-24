using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Module.Report.Application.Repository;
using ContactApp.Module.Report.Application.Services;
using ContactApp.Module.Report.Application.Services.Interfaces;
using ContactApp.Module.Report.Persistence.Context;
using ContactApp.Module.Report.Persistence.Repostiory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Module.Report.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<PGDataReportContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IExportService, XlsxExportService>();

            return services;
        }
    }
}
