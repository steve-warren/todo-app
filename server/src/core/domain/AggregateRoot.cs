using System.Collections.Generic;

namespace WarrenSoftware.TodoApp.Core.Domain
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot() { }
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void Apply(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}