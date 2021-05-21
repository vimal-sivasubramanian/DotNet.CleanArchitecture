using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Application.Models;
using DotNet.EventSourcing.Service.Domain;
using DotNet.EventSourcing.Service.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Infrastructure.Services
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ILogger<EventProcessor> _logger;
        private readonly IPublisher _mediator;

        public EventProcessor(ILogger<EventProcessor> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                if (@event is DomainEvent domainEvent)
                {
                    _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
                    await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
                }
                //else if (@event is IntegrationEvent integrationEvent) { }
            }
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(EventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}