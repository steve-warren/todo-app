using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ActiveTodoList : Entity
    {
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
    }

    public static class TodoListStates
    {
        public const string Active = "Active";
        public const string Archived = "Archived";
    }
}