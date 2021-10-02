namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class IncompleteTodoItem : TodoItem
    {
        public void Complete() => _state = TodoItemStates.Completed;
    }
}