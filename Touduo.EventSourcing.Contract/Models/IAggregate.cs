namespace Touduo.EventSourcing.Contract.Models;

public interface IAggregate
{
    public Guid Id { get; }
    public IReadOnlyList<IDomainEvent> UncommittedEvents { get; }
    public void ApplyEvent(IDomainEvent @event);
}