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

        public DbContextBase(DbContextOptions options, IEventBus eventBus) : base(options)
        {
            _eventBus = eventBus;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var domainEntities = ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Count > 0).ToList();

            var domainEvents = new List<IDomainEvent>();

            foreach (var entity in domainEntities)
                foreach (var domainEvent in entity.Entity.DomainEvents)
                    domainEvents.Add(domainEvent);

            await _eventBus.PublishAsync(domainEvents, cancellationToken).ConfigureAwait(false);
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            return result;
        }
    }
}