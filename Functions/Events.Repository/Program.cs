using DotNet.CleanArchitecture.Common.EventStore;
using Microsoft.Extensions.Hosting;

namespace Events.Repository
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
