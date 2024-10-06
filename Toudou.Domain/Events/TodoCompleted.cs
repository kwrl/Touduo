using Touduo.EventSourcing.Contract.Models;

namespace Toudou.Domain.Events;

public record TodoCompleted(Guid Id, Guid AggregateId) : IDomainEvent;