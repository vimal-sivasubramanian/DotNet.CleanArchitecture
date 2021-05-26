using DotNet.EventSourcing.Core;
using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Service.Domain.Entities;

namespace DotNet.EventSourcing.Service.Application.Persons.Events
{
    public class PersonEvent : EventBase
    {
        internal class Types
        {
            internal const string Created = "PersonCreated";
        }

        public PersonEvent(string eventName, Person person)
        {
            EventName = eventName;
            Payload = person.ToJson();
            CorrelationId = person.Id.ToString();
            Type = nameof(Person);
        }
    }
}
