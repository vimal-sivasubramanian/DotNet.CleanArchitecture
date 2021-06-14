using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces;
using DotNet.EventSourcing.MessageBrokers;
using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Infrastructure.Persistence;
using DotNet.EventSourcing.Service.Infrastructure.Services;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNet.EventSourcing.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(_ => { });

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireDb"))
                .UseRecommendedSerializerSettings();

            services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(
                           configuration.GetConnectionString("DefaultConnection"),
                           b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IEventPublisher, EventPublisher>();

            var messageBrokerOptions = configuration.GetSection(nameof(MessageBrokerOptions)).Get<MessageBrokerOptions>();
            if (messageBrokerOptions is not null)
                services.AddMessageBusSender<Guid, EventBase>(messageBrokerOptions);

            services.AddScoped<IBackgroundJobScheduler, HangfireBackgroundJobScheduler>();
            services.AddScoped<IBackgroundJobProcessor<IRequest>, BackgroundJobProcessor>();
        }
    }
}
