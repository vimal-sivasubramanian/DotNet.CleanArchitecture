using DotNet.EventSourcing.Core;
using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EventsStorage
{
    public class EventStorage
    {
        private readonly IEventStore _eventStore;

        public EventStorage(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [Function(nameof(EventStorage))]
        public void Run([EventHubTrigger("vs-eventsourcing-eventhub", Connection = "EventHubConnectionString")] string[] messages, FunctionContext context)
        {
            var logger = context.GetLogger("KafkaFunction");
            foreach (var message in messages)
            {
                logger.LogDebug($"C# Kafka trigger function processed a message: {message}");

                _eventStore.AppendAsync(message.To<EventBase>()).GetAwaiter().GetResult();
            }
        }
    }
}
