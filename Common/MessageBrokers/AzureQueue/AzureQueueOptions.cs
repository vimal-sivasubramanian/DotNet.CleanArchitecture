namespace DotNet.EventSourcing.MessageBrokers.AzureQueue
{
    public class AzureQueueOptions
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
