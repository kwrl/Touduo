using Touduo.EventSourcing.Contract.Models;

namespace Touduo.EventSourcing.Contract.Repositories;

public interface IAggregateRepository<TAggregate> where TAggregate : IAggregate
{
    Task<TAggregate> GetByIdAsync(Guid id);
    Task SaveAsync(TAggregate aggregate);
}