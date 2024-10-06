namespace Touduo.EventSourcing.Contract.Models;

public interface IDomainEvent
{
    Guid Id { get; }
    Guid AggregateId { get; }
}