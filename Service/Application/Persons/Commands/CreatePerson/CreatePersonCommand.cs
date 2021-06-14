using DotNet.CleanArchitecture.Service.Application.IdentityCards.Commands.CreateIdentityCard;
using DotNet.CleanArchitecture.Service.Application.Interfaces;
using DotNet.CleanArchitecture.Service.Application.Persons.Events;
using DotNet.CleanArchitecture.Service.Domain.Entities;
using DotNet.CleanArchitecture.Service.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Service.Application.Persons.Commands.CreatePerson
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
        private readonly IBackgroundJobScheduler _jobScheduler;

        public CreatePersonHandler(IApplicationDbContext dbContext, IEventPublisher eventPublisher, IBackgroundJobScheduler jobScheduler)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            _jobScheduler = jobScheduler ?? throw new ArgumentNullException(nameof(jobScheduler));
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

            _jobScheduler.EnqueueWithHighPriority("Create national identity card", new CreateIdentityCardCommand() { PersonId = person.Id });

            return Unit.Value;

        }
    }
}
