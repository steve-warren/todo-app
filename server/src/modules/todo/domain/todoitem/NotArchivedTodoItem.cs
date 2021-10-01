namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class NotArchivedTodoItem : TodoItem
    {
        public void Archive() => _archivedState = TodoItemArchiveStates.Archived;
    }
}