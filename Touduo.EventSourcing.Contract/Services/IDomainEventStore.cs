using Touduo.EventSourcing.Contract.Models;

namespace Touduo.EventSourcing.Contract.Services;

public interface IDomainEventStore
{
    IAsyncEnumerable<IDomainEvent> GetEvents(string streamId, CancellationToken cancellationToken = default);
    Task AppendEventAsync(string streamId, IDomainEvent @event, CancellationToken cancellationToken = default);
}