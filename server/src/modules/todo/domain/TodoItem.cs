using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoItem : AggregateRoot
    {
        public TodoItem(string name, int listId, int id)
        {
            Name = name;
            ListId = listId;
            Id = id;
        }

        private TodoItem() { }

        public int ListId { get; private set; }
        public string Name { get; private set; } = "";

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Relocate(int listId)
        {
            ListId = listId;
        }
    }

    public class TodoItemCreated : IDomainEvent
    {
        public int ListId { get; init; }
    }
}