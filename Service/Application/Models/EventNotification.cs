using DotNet.CleanArchitecture.Core.Events;
using MediatR;

namespace DotNet.CleanArchitecture.Service.Application.Models
{
    public class EventNotification<TEvent> : INotification where TEvent : EventBase
    {
        public TEvent Event { get; set; }

        public EventNotification(TEvent @event) => Event = @event;
    }
}
