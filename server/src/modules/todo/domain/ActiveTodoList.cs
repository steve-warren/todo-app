using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ActiveTodoList : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        private string _state = "";

        public ActiveTodoList(string name, int id = default)
        {
            Name = name;
            Id = id;
            _state = TodoListStates.Active;
        }

        private ActiveTodoList() { }

        public string Name { get; private set; } = "";

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Archive()
        {
            _state = TodoListStates.Archived;
        }

        public TodoItem NewItem(string name)
        {
            return new TodoItem(name, Id);
        }
    }
}