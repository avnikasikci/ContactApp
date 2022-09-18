using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Report.Application.Services;
using ContactApp.Module.Report.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {

            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });
            services.AddSingleton<IMongoDbSettings>(
                     sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IExportService, XlsxExportService>();

            return services;
        }
    }
}
