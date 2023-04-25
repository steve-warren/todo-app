using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain;

public record TodoItemCompletedState : Enumeration
{
    public static TodoItemCompletedState Parse(string name)
    {
        return name switch
        {
            nameof(Incomplete) => Incomplete,
            nameof(Completed) => Completed,
            _ => throw new ArgumentException("invalid name", nameof(name))
        };
    }

    public static readonly TodoItemCompletedState Incomplete = new() { Name = nameof(Incomplete) };
    public static readonly TodoItemCompletedState Completed = new() { Name = nameof(Completed) };
}
