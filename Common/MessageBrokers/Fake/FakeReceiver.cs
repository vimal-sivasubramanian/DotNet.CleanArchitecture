using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.Core.Models;
using System;

namespace DotNet.EventSourcing.MessageBrokers.Fake
{
    public class FakeReceiver<TKey, TValue> : IMessageReceiver<TKey, TValue>
    {
        public void Receive(Action<Message<TKey, TValue>> action)
        {
        }
    }
}
