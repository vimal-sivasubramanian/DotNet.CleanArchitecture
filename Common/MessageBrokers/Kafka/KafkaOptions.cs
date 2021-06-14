namespace DotNet.CleanArchitecture.MessageBrokers.Kafka
{
    public class KafkaOptions
    {
        public string BootstrapServers { get; set; }

        public string GroupId { get; set; }

        public string TopicName { get; set; }
    }
}
