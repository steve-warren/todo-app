using System;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http
{
    public class AddTodoItemViewModel
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Priority { get; set; }
        public DateTimeOffset? Reminder { get; set; }
    }
}