using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public abstract class DbContextBase : DbContext
    {
        private readonly IEventBus _eventBus;
        private bool _isPending = false;

        public DbContextBase(DbContextOptions options, IEventBus eventBus) : base(options)
        {
            _eventBus = eventBus;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_isPending is true)
                throw new InvalidOperationException($"Cannot make call to {nameof(SaveChangesAsync)} during a pending operation.");

            _isPending = true;

            try
            {
                do
                {
                    var domainEvents = new List<IDomainEvent>();

                    var domainEntities = ChangeTracker
                        .Entries<AggregateRoot>()
                        .Select(x => x.Entity);

                    foreach (var entity in domainEntities)
                    {
                        domainEvents.AddRange(entity.DomainEvents);
                        entity.ClearDomainEvents();
                    }

                    if (domainEvents.Count > 0)
                        await _eventBus.PublishAsync(domainEvents, cancellationToken).ConfigureAwait(false);
                    
                    else
                        break;
                } while(true);

                var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return result;
            }

            finally
            {
                _isPending = false;
            }
        }
    }
}