using DotNet.EventSourcing.Core;
using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces;
using DotNet.EventSourcing.Core.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;

namespace Events.Repository.Storage
{
    public class EventStorageFunction
    {
        private readonly IEventStore _eventStore;

        public EventStorageFunction(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [Function(nameof(EventStorageFunction))]
        public void Run([EventHubTrigger("vs-eventsourcing-eventhub", Connection = "EventHubConnectionString")] string[] messages, FunctionContext context)
        {
            var logger = context.GetLogger("EventStorage");
            foreach (var message in messages)
            {
                logger.LogDebug($"C# Kafka trigger function processed a message: {message}");

                _eventStore.AppendAsync(message.To<Message<Guid, EventBase>>().Value).GetAwaiter().GetResult();
            }
        }
    }
}
