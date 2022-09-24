using Microsoft.Extensions.DependencyInjection;
using GreenPipes;
using MassTransit;
using System;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Core.Application.Pipelines.Validation;
using ContactApp.Module.Report.Application.Consumer;
using ContactApp.Module.Report.Application.Job;
using ContactApp.Core.Application.Infrastructure.ImportExport;

namespace ContactApp.Module.Report.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<ICreateReportJobService, CreateReportJobService>();
            services.AddScoped<IExportService, XlsxExportService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ReportConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
                {
                    cur.UseHealthCheck(provider);
                    cur.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cur.ReceiveEndpoint("reportQueue", oq =>
                    {
                        oq.PrefetchCount = 20;
                        oq.UseMessageRetry(r => r.Interval(2, 100));
                        oq.ConfigureConsumer<ReportConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }

}
