namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class CompletedTodoItem : TodoItem
    {
        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Relocate(int listId)
        {
            ListId = listId;
        }
        
        public void Incomplete() => _state = TodoItemStates.Incomplete;
    }
}