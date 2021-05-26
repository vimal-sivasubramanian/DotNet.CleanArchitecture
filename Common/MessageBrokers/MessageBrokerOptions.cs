using DotNet.EventSourcing.MessageBrokers.AzureEventHub;
using DotNet.EventSourcing.MessageBrokers.AzureQueue;
using DotNet.EventSourcing.MessageBrokers.AzureServiceBus;
using DotNet.EventSourcing.MessageBrokers.Kafka;
using DotNet.EventSourcing.MessageBrokers.RabbitMQ;

namespace DotNet.EventSourcing.MessageBrokers
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
