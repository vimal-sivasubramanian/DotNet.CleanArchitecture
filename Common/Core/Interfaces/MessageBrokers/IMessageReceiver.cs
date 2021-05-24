using DotNet.EventSourcing.Core.Models;
using System;

namespace DotNet.EventSourcing.Core.Interfaces.MessageBrokers
{
    public interface IMessageReceiver<TKey, TValue>
    {
        void Receive(Action<Message<TKey, TValue>> action);
    }
}
