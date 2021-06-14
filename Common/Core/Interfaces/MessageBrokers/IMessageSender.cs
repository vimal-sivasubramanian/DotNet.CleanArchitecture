using DotNet.CleanArchitecture.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers
{
    public interface IMessageSender<TKey, TValue>
    {
        Task SendAsync(Message<TKey, TValue> message, CancellationToken cancellationToken = default);
    }
}
