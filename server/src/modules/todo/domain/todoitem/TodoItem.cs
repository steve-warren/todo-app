using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public abstract class TodoItem : AggregateRoot
    {
        protected string _state = "";
        protected string _archivedState = "";

        protected TodoItem() { }

        public int ListId { get; protected set; }
        public string Name { get; protected set; } = "";
    }
}