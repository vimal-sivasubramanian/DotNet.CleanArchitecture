using Azure.Storage.Queues;
using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using DotNet.CleanArchitecture.Core.Models;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.MessageBrokers.AzureQueue
{
    public class AzureQueueSender<TKey, TValue> : IMessageSender<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public AzureQueueSender(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }

        public Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            var queue = new QueueClient(_connectionString, _queueName);
            return queue.SendMessageAsync(JsonConvert.SerializeObject(message), cancellationToken);
        }
    }
}
