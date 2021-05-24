using Azure.Messaging.EventHubs.Consumer;
using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.Core.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.MessageBrokers.AzureEventHub
{
    public class AzureEventHubReceiver<TKey, TValue> : IMessageReceiver<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _hubName;
        private readonly string _consumerGroup;

        public AzureEventHubReceiver(string connectionString, string hubName, string consumerGroup)
        {
            _connectionString = connectionString;
            _hubName = hubName;
            _consumerGroup = consumerGroup;
        }

        public void Receive(Action<Message<TKey, TValue>> action)
        {
            ReceiveAsync(action).GetAwaiter().GetResult();
        }

        public async Task ReceiveAsync(Action<Message<TKey, TValue>> action)
        {
            //TODO: For production make use of processors
            // https://devblogs.microsoft.com/azure-sdk/eventhubs-clients/#choosing-a-client-from-azure-messaging-eventhubs

            await using var consumer = new EventHubConsumerClient(_consumerGroup, _connectionString, _hubName);
            try
            {
                while (true)
                {
                    // To ensure that we do not wait for an indeterminate length of time, we'll
                    // stop reading after we receive five events.  For a fresh Event Hub, those
                    // will be the first five that we had published.  We'll also ask for
                    // cancellation after 90 seconds, just to be safe.

                    using var cancellationSource = new CancellationTokenSource();
                    cancellationSource.CancelAfter(1000);

                    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
                    {
                        action(JsonConvert.DeserializeObject<Message<TKey, TValue>>(partitionEvent.Data.EventBody.ToString()));
                    }
                }
            }
            finally
            {
                await consumer.CloseAsync();
            }
        }
    }
}
