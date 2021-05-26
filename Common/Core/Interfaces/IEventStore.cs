using DotNet.EventSourcing.Core.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Core.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync(EventBase @event);

        Task<IAsyncEnumerable<EventBase>> ReadAsync(string entityName, string id);
    }
}
