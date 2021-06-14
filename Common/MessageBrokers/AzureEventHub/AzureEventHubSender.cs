using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using DotNet.CleanArchitecture.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.MessageBrokers.AzureEventHub
{
    public class AzureEventHubSender<TKey, TValue> : IMessageSender<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _hubName;

        public AzureEventHubSender(string connectionString, string hubName)
        {
            _connectionString = connectionString;
            _hubName = hubName;
        }

        public async Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            //TODO: For production make use of processors
            // https://devblogs.microsoft.com/azure-sdk/eventhubs-clients/#choosing-a-client-from-azure-messaging-eventhubs
            await using var producerClient = new EventHubProducerClient(_connectionString, _hubName);
            using var eventBatch = await producerClient.CreateBatchAsync(cancellationToken);
            var payload = JsonConvert.SerializeObject(message);
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(payload)));
            await producerClient.SendAsync(eventBatch, cancellationToken);
        }
    }
}
