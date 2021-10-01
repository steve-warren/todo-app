using System;
using System.Collections.Generic;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public class ActiveTodoList : TodoList
    {
        private readonly List<int> _items;

        public ActiveTodoList(string name, int id)
        {
            Name = name;
            Id = id;
            _items = new();
        }

        public string Name { get; private set; } = "";
        public IReadOnlyCollection<int> Items => _items;

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void Archive()
        {
            _state = TodoListStates.Archived;
        }

        public void AddItem(int itemId)
        {
            if (_items.Contains(itemId)) throw new InvalidOperationException("Attempting to add item to list when it already exists.");

            _items.Insert(index: 0, itemId);
        }

        public void MoveItem(int itemId, int position)
        {
            var currentIndex = _items.IndexOf(itemId);

            if (currentIndex is -1) return;
            if (position < 0 || position > _items.Count - 1) throw new ArgumentOutOfRangeException(nameof(position));

            _items.RemoveAt(currentIndex);
            _items.Insert(index: position, itemId);
        }
    }
}