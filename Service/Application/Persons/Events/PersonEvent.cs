using DotNet.EventSourcing.Service.Domain.Entities;
using DotNet.EventSourcing.Service.Domain.Events;

namespace DotNet.EventSourcing.Service.Application.Persons.Events
{
    public class PersonEvent : DomainEvent
    {
        internal class Types
        {
            internal const string Created = "PersonCreated";
        }

        public PersonEvent(string eventName, Person person)
        {
            EventName = eventName;
            Entity = person.ToJson();
            EntityType = nameof(Person);
        }
    }
}
