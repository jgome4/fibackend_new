using System;
using Microsoft.Extensions.Logging;
using MediatR;
using FI_Domain;

namespace FI_Aplication_Events_Domain;

    public class DispatcherTask : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DispatcherTask> _log;
        public DispatcherTask(IMediator mediator, ILogger<DispatcherTask> log)
        {
            _mediator = mediator;
            _log = log;
        }

        public async System.Threading.Tasks.Task Dispatch(IDomainEvent devent)
        {

            var domainEventNotification = _createDomainEventNotification(devent);
            _log.LogDebug("Dispatching Domain Event as MediatR notification.  EventType: {eventType}", devent.GetType());
            await _mediator.Publish(domainEventNotification);
        }
       
        private INotification _createDomainEventNotification(IDomainEvent domainEvent)
        {
            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);

        }
    }

