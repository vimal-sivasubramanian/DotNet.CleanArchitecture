using DotNet.EventSourcing.Core.Events;
using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.Service.Application.Interfaces;
using DotNet.EventSourcing.Service.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNet.EventSourcing.Service.Infrastructure.Services
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;
        private readonly IPublisher _mediator;
        private readonly IMessageSender<Guid, EventBase> _messageSender;

        public EventPublisher(ILogger<EventPublisher> logger, IPublisher mediator, IMessageSender<Guid, EventBase> messageSender)
        {
            _logger = logger;
            _mediator = mediator;
            _messageSender = messageSender;
        }

        public async Task Publish(params EventBase[] events)
        {
            foreach (var @event in events)
            {
                _logger.LogInformation($"Publishing {@event.GetType().Name} event");
                await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(@event));
                await _messageSender.SendAsync(new Core.Models.Message<Guid, EventBase> { Key = Guid.NewGuid(), Value = @event });

            }
        }

        private INotification GetNotificationCorrespondingToDomainEvent(EventBase @event)
        {
            return (INotification)Activator.CreateInstance(
                typeof(EventNotification<>).MakeGenericType(@event.GetType()), @event);
        }
    }
}