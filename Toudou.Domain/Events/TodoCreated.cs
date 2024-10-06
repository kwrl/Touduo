using Touduo.EventSourcing.Contract.Models;

namespace Toudou.Domain.Events;

public record TodoCreated(Guid Id, Guid AggregateId, string Description) : IDomainEvent;