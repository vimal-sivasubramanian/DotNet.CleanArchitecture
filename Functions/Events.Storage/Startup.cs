using DotNet.EventSourcing.Common.EventStore;
using DotNet.EventSourcing.Events.Storage;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DotNet.EventSourcing.Events.Storage
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddEventStore();
        }
    }
}
