using DotNet.EventSourcing.Common.EventStore;
using Microsoft.Extensions.Hosting;

namespace EventsStorage
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(b =>
                {
                    b.Services.AddEventStore();
                })
                .Build();
            host.Run();
        }
    }
}
