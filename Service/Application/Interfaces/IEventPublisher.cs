using DotNet.CleanArchitecture.Core.Events;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Service.Application.Interfaces
{

    public interface IEventPublisher
    {
        Task Publish(params EventBase[] events);
    }

}
