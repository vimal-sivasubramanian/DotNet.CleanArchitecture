using DotNet.EventSourcing.Service.Domain.Events;
using MediatR;

namespace DotNet.EventSourcing.Service.Application.Models
{
    public class EventNotification<TEvent> : INotification where TEvent : class, IEvent
    {
    }
}
