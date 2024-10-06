using Touduo.EventSourcing.Contract.Models;

namespace Toudou.Domain.Events;

public record TodoCancelled(Guid Id, Guid AggregateId) : IDomainEvent;