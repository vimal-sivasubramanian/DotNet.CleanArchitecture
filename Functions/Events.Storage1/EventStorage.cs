using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces;
using DotNet.EventSourcing.Core.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Events.Storage
{
    public class EventStorage
    {
        private readonly IEventStore _eventStore;

        public EventStorage(IEventStore eventStore) => _eventStore = eventStore;

        [FunctionName(nameof(EventStorage))]
        public async ValueTask Run([RabbitMQTrigger("app-events", ConnectionStringSetting = "RabbitMqConnection")]string eventPayload, ILogger logger)
        {
            var @event = JsonConvert.DeserializeObject<Message<dynamic, EventBase>>(eventPayload);

            logger.LogDebug($"Attempting to append the {@event.Value.EventName} event to EventStore");

            await _eventStore.AppendAsync(@event.Value);
        }
    }
}
