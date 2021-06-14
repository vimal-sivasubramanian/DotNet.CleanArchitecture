using DotNet.CleanArchitecture.Core.Models;
using System;

namespace DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers
{
    public interface IMessageReceiver<TKey, TValue>
    {
        void Receive(Action<Message<TKey, TValue>> action);
    }
}
