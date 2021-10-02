using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoItem : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        protected string _state = "";

        public TodoItem(string name, int listId, TodoItemPriority priority, int id)
        {
            Name = name;
            ListId = listId;
            Priority = priority;
            Id = id;

            Apply(new TodoItemCreated { Name = name, ListId = listId, Id = id, Priority = priority, Reminder = Reminder });
        }

        protected TodoItem() { }

        public int ListId { get; private set; }
        public string Name { get; private set; } = "";
        public string Notes { get; private set; } = "";
        public DateTimeOffset? Reminder { get; private set; }
        public TodoItemPriority Priority { get; private set; } = TodoItemPriority.None;

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Relocate(int listId)
        {
            ListId = listId;
        }
    }
}