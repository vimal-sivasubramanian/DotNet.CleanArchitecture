using Confluent.Kafka;
using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.MessageBrokers.Kafka
{
    public class KafkaSender<TKey, TValue> : IMessageSender<TKey, TValue>, IDisposable
    {
        private readonly string _topic;
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaSender(string bootstrapServers, string topic)
        {
            _topic = topic;

            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public void Dispose()
        {
            _producer.Flush(TimeSpan.FromSeconds(10));
            _producer.Dispose();
        }

        public Task SendAsync(Core.Models.Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            return _producer.ProduceAsync(_topic, new Message<TKey, TValue>
            {
                Key = message.Key,
                Value = message.Value
            }, cancellationToken);
        }
    }
}
