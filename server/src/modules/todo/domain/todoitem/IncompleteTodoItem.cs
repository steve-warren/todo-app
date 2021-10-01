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

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Relocate(int listId)
        {
            ListId = listId;
        }

        public void Complete() => _state = TodoItemStates.Completed;
    }
}