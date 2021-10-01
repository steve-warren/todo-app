namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class IncompleteTodoItem : TodoItem
    {
        public IncompleteTodoItem(string name, int listId, int id)
        {
            Name = name;
            ListId = listId;
            Id = id;

            Apply(new TodoItemCreated { Name = name, ListId = listId, Id = id });
        }

        public void Complete() => _state = TodoItemStates.Completed;

        public void Archive() => _state = TodoItemStates.Archived;
    }
}