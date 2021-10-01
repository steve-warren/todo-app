using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoItemCreated : IDomainEvent
    {
        public string Name { get; init; } = "";
        public int ListId { get; init; }
        public int Id { get; init; }
    }
}