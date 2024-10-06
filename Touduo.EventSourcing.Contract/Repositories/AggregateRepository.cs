using Touduo.EventSourcing.Contract.Models;
using Touduo.EventSourcing.Contract.Services;

namespace Touduo.EventSourcing.Contract.Repositories;

public class AggregateRepository<TAggregate>(IDomainEventStore domainEventStore) : IAggregateRepository<TAggregate>
    where TAggregate : IAggregate
{
    public async Task<TAggregate> GetByIdAsync(Guid id)
    {
        var streamId = GetStreamId(id);
        var aggregate = (TAggregate)(Activator.CreateInstance(typeof(TAggregate)) ?? throw new Exception());
        
        var events = domainEventStore.GetEvents(streamId);
        
        await foreach (var @event in events)
        {
            aggregate.ApplyEvent(@event);
        }
        
        return aggregate;
    }

    public async Task SaveAsync(TAggregate aggregate)
    {
        var streamId = GetStreamId(aggregate.Id);
       
        foreach (var @event in aggregate.UncommittedEvents)
        {
            await domainEventStore.AppendEventAsync(streamId, @event);
        }
    }
    
    private static string GetStreamId(Guid id) => $"{typeof(TAggregate).Name}-{id}";
}