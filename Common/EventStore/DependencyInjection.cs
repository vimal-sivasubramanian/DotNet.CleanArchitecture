using DotNet.CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EventStoreImpl = DotNet.CleanArchitecture.Common.EventStore.Services.EventStore;

namespace DotNet.CleanArchitecture.Common.EventStore
{
    public static class DependencyInjection
    {
        public static void AddEventStore(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStoreImpl>();
        }
    }
}
