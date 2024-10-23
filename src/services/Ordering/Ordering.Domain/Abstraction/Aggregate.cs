namespace Ordering.Domain.Abstraction
{
    public class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> Events => _domainEvents.AsReadOnly();

        public void AddDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);

        }

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] domainEventsQueue = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEventsQueue;

        }
    }
}
