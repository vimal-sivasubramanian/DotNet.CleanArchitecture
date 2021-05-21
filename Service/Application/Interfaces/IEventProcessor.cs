using DotNet.EventSourcing.Service.Domain.Events;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Application.Interfaces
{

    public interface IEventProcessor
    {
        Task Publish(params IEvent[] events);
    }

}
