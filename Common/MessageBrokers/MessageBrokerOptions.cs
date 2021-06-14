using DotNet.CleanArchitecture.MessageBrokers.AzureEventHub;
using DotNet.CleanArchitecture.MessageBrokers.AzureQueue;
using DotNet.CleanArchitecture.MessageBrokers.AzureServiceBus;
using DotNet.CleanArchitecture.MessageBrokers.Kafka;
using DotNet.CleanArchitecture.MessageBrokers.RabbitMQ;

namespace DotNet.CleanArchitecture.MessageBrokers
{
    public class MessageBrokerOptions
    {
        public string Provider { get; set; }

        public RabbitMQOptions RabbitMQ { get; set; }

        public KafkaOptions Kafka { get; set; }

        public AzureQueueOptions AzureQueue { get; set; }

        public AzureServiceBusOptions AzureServiceBus { get; set; }

        public AzureEventHubOptions AzureEventHub { get; set; }
    }
}
