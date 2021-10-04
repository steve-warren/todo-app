using System;
using System.Collections.Generic;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class TodoList : AggregateRoot
    {
        #pragma warning disable IDE0052 // mapped to ef core shadow property
        private ArchiveState _archiveState = ArchiveState.NotArchived;
        private readonly List<int> _items;

        public TodoList(string name, int id, int ownerId)
        {
            Name = name;
            Id = id;
            OwnerId = ownerId;
            _items = new();
        }

        public int OwnerId { get; private set; }
        public string Name { get; private set; } = "";
        public IReadOnlyCollection<int> Items => _items;

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void AddItem(int itemId)
        {
            if (_items.Contains(itemId)) throw new InvalidOperationException("Attempting to add item to list when it already exists.");

            _items.Insert(index: 0, itemId);
        }

        public void ArrangeItem(int itemId, int position)
        {
            var currentIndex = _items.IndexOf(itemId);

            if (currentIndex is -1) return;
            if (position < 0 || position > _items.Count - 1) throw new ArgumentOutOfRangeException(nameof(position));

            _items.RemoveAt(currentIndex);
            _items.Insert(index: position, itemId);
        }

        public void Archive()
        {
            _archiveState = ArchiveState.Archived;

            Apply(new TodoListArchived { Name = this.Name, Id = this.Id });
        }
    }
}