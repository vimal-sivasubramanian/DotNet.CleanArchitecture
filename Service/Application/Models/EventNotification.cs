using DotNet.EventSourcing.Service.Domain.Events;
using MediatR;

namespace DotNet.EventSourcing.Service.Application.Models
{
    public class EventNotification<TEvent> : INotification where TEvent : class, IEvent
    {
        public TEvent Event { get; set; }

        public EventNotification(TEvent @event) => Event = @event;
    }
}
