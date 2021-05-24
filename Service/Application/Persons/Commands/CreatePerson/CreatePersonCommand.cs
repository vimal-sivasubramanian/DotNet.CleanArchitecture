using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Application.Persons.Events;
using DotNet.EventSourcing.Service.Domain.Entities;
using DotNet.EventSourcing.Service.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }

    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;

        public CreatePersonHandler(IApplicationDbContext dbContext, IEventPublisher eventPublisher)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender
            };

            _dbContext.Persons.Add(person);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _eventPublisher.Publish(new PersonEvent(PersonEvent.Types.Created, person));

            return Unit.Value;

        }
    }
}
