using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Service.Application.IdentityCards.Commands.CreateIdentityCard
{
    public class CreateIdentityCardCommand : IRequest
    {
        public int PersonId { get; set; }
    }

    public class CreateIdentityCardHandler : IRequestHandler<CreateIdentityCardCommand, Unit>
    {
        public Task<Unit> Handle(CreateIdentityCardCommand request, CancellationToken cancellationToken)
        {
            return Task.Delay(TimeSpan.FromSeconds(30), cancellationToken).ContinueWith(t => Unit.Value);
        }
    }
}
