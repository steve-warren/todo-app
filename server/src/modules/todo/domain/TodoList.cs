using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public abstract class TodoList : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        protected string _state = "";

        protected TodoList(int id = default)
        {
            Id = id;
            _state = TodoListStates.Active;
        }

        protected TodoList() { }
    }
}