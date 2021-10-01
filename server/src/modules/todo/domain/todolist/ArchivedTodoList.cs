namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ArchivedTodoList : TodoList
    {
        public void Restore()
        {
            _state = TodoListStates.Active;
        }
    }
}