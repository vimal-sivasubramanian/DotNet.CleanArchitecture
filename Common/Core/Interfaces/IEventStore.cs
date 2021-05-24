using DotNet.EventSourcing.Core.Events;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Core.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync(EventBase @event);
    }
}
