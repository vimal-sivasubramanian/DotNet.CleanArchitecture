using DotNet.EventSourcing.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Core.Interfaces.MessageBrokers
{
    public interface IMessageSender<TKey, TValue>
    {
        Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default);
    }
}
