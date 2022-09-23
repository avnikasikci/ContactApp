using ContactApp.Core.Application.Infrastructure.ImportExport;
using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Application.Services;
using ContactApp.Module.User.Application.Services.Interfaces;
using ContactApp.Module.User.Persistence.Context;
using ContactApp.Module.User.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Persistence
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


            services.AddDbContext<PGDataUserContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Default"));
            });

            services.AddSingleton<IMongoDbSettings>(
                     sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContactInformationService, UserContactInformationService>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserContactInformationRepository, UserContactInformationRepository>();

            return services;
        }
    }
}
