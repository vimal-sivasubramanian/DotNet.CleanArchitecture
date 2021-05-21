using DotNet.EventSourcing.Service.Domain.Enums;

namespace DotNet.EventSourcing.Service.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
