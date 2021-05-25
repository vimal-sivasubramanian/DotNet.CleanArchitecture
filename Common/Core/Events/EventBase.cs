using System;

namespace DotNet.EventSourcing.Core.Events
{
    public class EventBase
    {
        public string EventName { get; set; }

        public string Type { get; set; }

        public string Payload { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
