using Azure.Storage.Queues;
using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using DotNet.CleanArchitecture.Core.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.MessageBrokers.AzureQueue
{
    public class AzureQueueReceiver<TKey, TValue> : IMessageReceiver<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public AzureQueueReceiver(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }

        public void Receive(Action<Message<TKey, TValue>> action)
        {
            Task.Factory.StartNew(() => ReceiveAsync(action));
        }

        private async Task ReceiveAsync(Action<Message<TKey, TValue>> action)
        {
            var queue = new QueueClient(_connectionString, _queueName);

            while (true)
            {
                var retrievedMessage = (await queue.ReceiveMessageAsync()).Value;

                if (retrievedMessage != null)
                {
                    action(JsonConvert.DeserializeObject<Message<TKey, TValue>>(retrievedMessage.Body.ToString()));
                    await queue.DeleteMessageAsync(retrievedMessage.MessageId, retrievedMessage.PopReceipt);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
