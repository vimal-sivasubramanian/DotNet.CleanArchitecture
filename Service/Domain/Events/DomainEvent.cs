using System;

namespace DotNet.EventSourcing.Service.Domain.Events
{
    public abstract class DomainEvent : IEvent
    {
        public string EventName { get; set; }

        public string EntityType { get; set; }

        public string Entity { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
