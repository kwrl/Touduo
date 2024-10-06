using Shouldly;
using Toudou.Domain.Models;

namespace Toudou.Domain.Tests;

public class TodoTests
{
    [Fact]
    public void InitiallyTheStatusOfATodoIsPending()
    {
        var underTest = Todo.Create(Guid.NewGuid(), "Do the dishes");
        
        underTest.Status.ShouldBe(TodoStatus.Pending);
    }
    
    [Fact]
    public void AfterGettingCompletedTheStatusOfATodoIsCompleted()
    {
        var underTest = Todo.Create(Guid.NewGuid(), "Do the dishes");
        underTest.Complete();
        
        underTest.Status.ShouldBe(TodoStatus.Completed);
    }
    
    [Fact]
    public void AfterGettingCancelledTheStatusOfATodoIsCancelled()
    {
        var underTest = Todo.Create(Guid.NewGuid(), "Do the dishes");
        underTest.Cancel();
        
        underTest.Status.ShouldBe(TodoStatus.Cancelled);
    }
}