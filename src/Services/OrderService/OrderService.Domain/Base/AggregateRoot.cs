using OrderService.Domain.Base.Interfaces;

namespace OrderService.Domain.Base;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _events = new();

    public IReadOnlyCollection<IDomainEvent> Events => _events;

    protected void AddDomainEvent(IDomainEvent @event)
        => _events.Add(@event);
}
