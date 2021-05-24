using DotNet.EventSourcing.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EventStoreImpl = DotNet.EventSourcing.Common.EventStore.Services.EventStore;

namespace DotNet.EventSourcing.Common.EventStore
{
    public static class DependencyInjection
    {
        public static void AddEventStore(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStoreImpl>();
        }
    }
}
