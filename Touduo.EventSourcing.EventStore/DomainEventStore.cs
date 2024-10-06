using System.Text.Json;
using EventStore.Client;
using Touduo.EventSourcing.Contract.Models;
using Touduo.EventSourcing.Contract.Services;

namespace Touduo.EventSourcing.EventStore;

public class DomainEventStore : IDomainEventStore
{
    private readonly EventStoreClient _client;

    public DomainEventStore(EventStoreClient client)
    {
        _client = client;
    }

    public IAsyncEnumerable<IDomainEvent> GetEvents(string streamId, CancellationToken cancellationToken = default)
    {
        var events = _client.ReadStreamAsync(
            Direction.Forwards,
            streamId,
            StreamPosition.Start,
            resolveLinkTos: true,
            cancellationToken: cancellationToken
        );

        return events.Select(DeserializeEvent);
    }
    
    private IDomainEvent DeserializeEvent(ResolvedEvent resolvedEvent)
    {
        var eventType = Type.GetType(resolvedEvent.Event.EventType) ?? throw new Exception("It broke");
        var evt = JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, eventType) as IDomainEvent;
        
        if (evt is null)
        {
            throw new Exception("It broke");
        }

        return evt;
    }
    


    public Task AppendEventAsync(string streamId, IDomainEvent @event, CancellationToken cancellationToken = default)
    {
        var eventData = JsonSerializer.SerializeToUtf8Bytes(@event);
        var eventToAppend = new EventData(Uuid.NewUuid(), @event.GetType().FullName, eventData);
        
        return _client.AppendToStreamAsync(streamId, StreamState.Any, [eventToAppend], cancellationToken: cancellationToken);
    }
}