using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Application.Services;
using ContactApp.Module.User.Application.Services.Interfaces;
using ContactApp.Module.User.Persistence.Context;
using ContactApp.Module.User.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Module.User.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<PGDataUserContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContactInformationService, UserContactInformationService>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserContactInformationRepository, UserContactInformationRepository>();

            return services;
        }
    }
}
