using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using DotNet.CleanArchitecture.Core.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusSender<TKey, TValue> : IMessageSender<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public AzureServiceBusSender(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }

        public Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            var queueClient = new QueueClient(_connectionString, _queueName);
            var bytes = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Value)))
            {
                MessageId = JsonConvert.SerializeObject(message.Key)
            };
            return queueClient.SendAsync(bytes);
        }
    }
}
