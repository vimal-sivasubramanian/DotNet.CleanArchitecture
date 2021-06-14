using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.MessageBrokers.Fake
{
    public class FakeSender<TKey, TValue> : IMessageSender<TKey, TValue>
    {
        public Task SendAsync(Core.Models.Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
