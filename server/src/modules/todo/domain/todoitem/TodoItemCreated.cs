using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain;

public class TodoItemCreated : IDomainEvent
{
    public string Name { get; init; } = "";
    public int ListId { get; init; }
    public int ItemId { get; init; }
    public DateTimeOffset? Reminder { get; init; }
    public TodoItemPriority Priority { get; init; } = TodoItemPriority.None;
}
