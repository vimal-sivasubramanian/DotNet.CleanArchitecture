using System.Collections.Generic;

namespace DotNet.EventSourcing.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusOptions
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
