using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoItem : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        protected string _state = "";

        public TodoItem(string name, int listId, int ownerId, TodoItemPriority priority, int id, string notes, DateTimeOffset? reminder)
        {
            Name = name;
            ListId = listId;
            OwnerId = ownerId;
            Priority = priority;
            Id = id;
            Notes = notes;
            Reminder = reminder;
            _state = TodoItemStates.Incomplete;

            Apply(new TodoItemCreated { Name = name, ListId = listId, Id = id, Priority = priority, Reminder = Reminder });
        }

        protected TodoItem() { }

        public int ListId { get; private set; }
        public int OwnerId { get; private set; }
        public string Name { get; private set; } = "";
        public string Notes { get; private set; } = "";
        public DateTimeOffset? Reminder { get; private set; }
        public TodoItemPriority Priority { get; private set; } = TodoItemPriority.None;

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Relocate(int listId)
        {
            ListId = listId;
        }

        public void SetReminder(DateTimeOffset? when)
        {
            Reminder = when;
        }

        public void ChangePriority(TodoItemPriority priority)
        {
            Priority = priority;
        }

        public void WriteNotes(string text)
        {
            Notes = text;
        }
    }
}