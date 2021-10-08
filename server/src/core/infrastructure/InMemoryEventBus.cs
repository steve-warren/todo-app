using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public InMemoryEventBus(IMediator mediator, ILogger<InMemoryEventBus> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogDebug($"Dispatching event '{domainEvent.GetType().Name}'");
            return _mediator.Publish(domainEvent, cancellationToken);
        }

        public async Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            foreach(var domainEvent in domainEvents)
            {
                _logger.LogDebug($"Dispatching event '{domainEvent.GetType().Name}'");

                await _mediator.Publish(domainEvent, CancellationToken.None);
            }
        }
    }
}