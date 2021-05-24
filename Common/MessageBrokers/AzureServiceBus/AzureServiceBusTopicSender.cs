using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.Core.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusTopicSender<TKey, TValue> : IMessageSender<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _topicName;

        public AzureServiceBusTopicSender(string connectionString, string topicName)
        {
            _connectionString = connectionString;
            _topicName = topicName;
        }

        public Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            var topicClient = new TopicClient(_connectionString, _topicName);
            var bytes = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Value)))
            {
                MessageId = JsonConvert.SerializeObject(message.Key)
            };
            return topicClient.SendAsync(bytes);
        }
    }
}
