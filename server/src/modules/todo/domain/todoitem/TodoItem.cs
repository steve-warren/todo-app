using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoItem : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        protected string _state = "";

        public TodoItem(string name, int listId, int id)
        {
            Name = name;
            ListId = listId;
            Id = id;

            Apply(new TodoItemCreated { Name = name, ListId = listId, Id = id });
        }

        protected TodoItem() { }

        public int ListId { get; protected set; }
        public string Name { get; protected set; } = "";

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