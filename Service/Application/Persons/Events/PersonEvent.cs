using DotNet.CleanArchitecture.Core;
using DotNet.CleanArchitecture.Core.Events;
using DotNet.CleanArchitecture.Service.Domain.Entities;

namespace DotNet.CleanArchitecture.Service.Application.Persons.Events
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
