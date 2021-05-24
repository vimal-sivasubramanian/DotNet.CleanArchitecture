using DotNet.EventSourcing.Core.Events;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Application.Interfaces
{

    public interface IEventPublisher
    {
        Task Publish(params EventBase[] events);
    }

}
