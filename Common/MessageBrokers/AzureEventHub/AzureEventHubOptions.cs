namespace DotNet.CleanArchitecture.MessageBrokers.AzureEventHub
{
    public class AzureEventHubOptions
    {
        public string ConnectionString { get; set; }

        public string ConsumerGroup { get; set; }

        public string HubName { get; set; }
    }
}
