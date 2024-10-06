using Toudou.Domain.Events;

namespace Toudou.Domain.Models;

public class Todo : Aggregate
{
    public string Description { get; private set; } = string.Empty;
    public TodoStatus Status { get; private set; }
    
    public void Apply(TodoCreated @event)
    {
        Id = @event.Id;
        Description = @event.Description;
        Status = TodoStatus.Pending;
    }
    
    public void Apply(TodoCompleted @event)
    {
        Status = TodoStatus.Completed;
    }
    
    public void Apply(TodoCancelled @event)
    {
        Status = TodoStatus.Cancelled;
    }
    
    public static Todo Create(Guid id, string description)
    {
        var todo = new Todo();
        todo.RaiseEvent(new TodoCreated(Guid.NewGuid(), id, description));
        return todo;
    }
    
    public void Complete()
    {
        if (Status == TodoStatus.Completed)
        {
            throw new InvalidOperationException("Todo is already completed");
        }
        
        RaiseEvent(new TodoCompleted(Guid.NewGuid(), Id));
    }
    
    public void Cancel()
    {
        if (Status == TodoStatus.Cancelled)
        {
            throw new InvalidOperationException("Todo is already cancelled");
        }
        
        RaiseEvent(new TodoCancelled(Guid.NewGuid(), Id));
    }
}