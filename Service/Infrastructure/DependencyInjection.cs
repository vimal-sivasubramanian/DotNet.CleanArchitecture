using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNet.EventSourcing.Service.Infrastructure.Persistence;

namespace DotNet.EventSourcing.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(
                           configuration.GetConnectionString("DefaultConnection"),
                           b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IEventProcessor, EventProcessor>();
        }
    }
}
