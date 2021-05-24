using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Common.EventStore.Services
{
    internal class EventStore : IEventStore, IDisposable
    {
        private readonly EventStoreClient _client;

        public EventStore()
        {
            _client = new EventStoreClient(
                EventStoreClientSettings.Create("esdb://localhost:2113?tls=false")
            );
        }

        public Task AppendAsync(EventBase @event)
        {
            return _client.AppendToStreamAsync(@event.Type,
                StreamState.NoStream,
                new List<EventData> {
                    new EventData( Uuid.NewUuid(), @event.EventName, Encoding.UTF8.GetBytes(@event.Payload))
                });
        }

        public void Dispose() => _client?.Dispose();
    }
}
