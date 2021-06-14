namespace DotNet.CleanArchitecture.MessageBrokers.AzureQueue
{
    public class AzureQueueOptions
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
