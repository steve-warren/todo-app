namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public static class TodoItemStates
    {
        public const string Incomplete = nameof(Incomplete);
        public const string Completed = nameof(Completed);
    }

    public static class TodoItemArchiveStates
    {
        public const string Archived = nameof(Archived);
        public const string NotArchived = nameof(NotArchived);
    }
}