using System;

namespace DotNet.CleanArchitecture.Core.Events
{
    public class EventBase
    {
        public string EventName { get; set; }

        public string Type { get; set; }

        public string Payload { get; set; }

        public string CorrelationId { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
