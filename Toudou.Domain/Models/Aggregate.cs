using Touduo.EventSourcing.Contract.Models;

namespace Toudou.Domain.Models;

public abstract class Aggregate : IAggregate
{
    public Guid Id { get; protected set; }
    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();
    private readonly List<IDomainEvent> _uncommittedEvents = new();

    public void ApplyEvent(IDomainEvent @event)
    {
        var method = GetType().GetMethod("Apply", [@event.GetType()]);
        
        if (method is null)
        {
            throw new InvalidOperationException($"No Apply method found for event {@event.GetType().Name}");
        }
        
        method.Invoke(this, [@event]);
    }

    protected void RaiseEvent(IDomainEvent @event)
    {
        ApplyEvent(@event);
        _uncommittedEvents.Add(@event);
    }
}