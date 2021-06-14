using DotNet.CleanArchitecture.Core.Interfaces.MessageBrokers;
using DotNet.CleanArchitecture.Core.Models;
using System;

namespace DotNet.CleanArchitecture.MessageBrokers.Fake
{
    public class FakeReceiver<TKey, TValue> : IMessageReceiver<TKey, TValue>
    {
        public void Receive(Action<Message<TKey, TValue>> action)
        {
        }
    }
}
