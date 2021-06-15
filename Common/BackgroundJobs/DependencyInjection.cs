using DotNet.CleanArchitecture.Core.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.CleanArchitecture.Common.BackgroundJobs
{
    public static class DependencyInjection
    {
        public static void AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(_ => { });

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireDb"))
                .UseRecommendedSerializerSettings();
            services.AddScoped<IBackgroundJobScheduler<IRequest>, BackgroundJobScheduler>();
            services.AddScoped<IBackgroundJobProcessor<IRequest>, BackgroundJobProcessor>();
        }
    }
}
