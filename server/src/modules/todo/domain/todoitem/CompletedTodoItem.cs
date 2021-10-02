namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class CompletedTodoItem : TodoItem
    {
        public void Incomplete() => _state = TodoItemStates.Incomplete;
    }
}