using DotNet.EventSourcing.Service.Application.Interfaces;
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

        public CreatePersonHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Persons.Add(new Domain.Entities.Person
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
