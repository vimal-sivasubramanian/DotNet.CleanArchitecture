using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.Core.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusSubscriptionReceiver<TKey, TValue> : IMessageReceiver<TKey, TValue>
    {
        private readonly string _connectionString;
        private readonly string _topicName;
        private readonly string _subscriptionName;
        private readonly SubscriptionClient _subscriptionClient;

        public AzureServiceBusSubscriptionReceiver(string connectionString, string topicName, string subscriptionName)
        {
            _connectionString = connectionString;
            _topicName = topicName;
            _subscriptionName = subscriptionName;
            _subscriptionClient = new SubscriptionClient(_connectionString, _topicName, _subscriptionName);
        }

        public void Receive(Action<Message<TKey, TValue>> action)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            };

            _subscriptionClient.RegisterMessageHandler((Message message, CancellationToken token) =>
            {
                action(new Message<TKey, TValue>
                {
                    Key = JsonConvert.DeserializeObject<TKey>(message.MessageId),
                    Value = JsonConvert.DeserializeObject<TValue>(Encoding.UTF8.GetString(message.Body))
                });
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
