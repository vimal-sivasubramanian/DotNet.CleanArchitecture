using DotNet.EventSourcing.Service.Application.Models;
using DotNet.EventSourcing.Service.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Application.Common.EventHandlers
{
    public class GenericDomainEventHandler : INotificationHandler<EventNotification<DomainEvent>>
    {
        public Task Handle(EventNotification<DomainEvent> notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
