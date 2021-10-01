namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ArchivedTodoItem : TodoItem
    {
        public void Restore() => _archivedState = TodoItemArchiveStates.NotArchived;
    }
}