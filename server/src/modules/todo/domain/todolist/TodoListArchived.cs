using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain;

public class TodoListArchived : IDomainEvent
{
    public string Name { get; init; } = "";
    public int Id { get; init; }
    public int OwnerId { get; init; }
}
