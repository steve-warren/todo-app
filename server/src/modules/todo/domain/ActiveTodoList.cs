using System;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ActiveTodoList : TodoList
    {
        public ActiveTodoList(string name, int id = default) : base(id)
        {
            Name = name;
        }

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