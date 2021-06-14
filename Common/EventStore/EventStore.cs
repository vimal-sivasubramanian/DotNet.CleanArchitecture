using DotNet.CleanArchitecture.Core.Events;
using DotNet.CleanArchitecture.Core.Interfaces;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Common.EventStore.Services
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
            return _client.AppendToStreamAsync($"{@event.Type}.{@event.CorrelationId}",
                StreamState.Any,
                new List<EventData> {
                    new EventData(Uuid.NewUuid(), @event.EventName, Encoding.UTF8.GetBytes(@event.Payload))
                });
        }

        public Task<IAsyncEnumerable<EventBase>> ReadAsync(string entityName, string id)
        {
            return Task.FromResult(AsyncEnumerable.Empty<EventBase>());
        }

        public void Dispose() => _client?.Dispose();
    }
}
