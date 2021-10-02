using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public record TodoItemPriority : Enumeration
    {
        public static TodoItemPriority Parse(string name) => name switch
        {
            nameof(None) => None,
            nameof(Low) => Low,
            nameof(Medium) => Medium,
            nameof(High) => High,
            _ => throw new ArgumentException("invalid name", nameof(name))
        };

        public static readonly TodoItemPriority None = new() { Name = nameof(None) };
        public static readonly TodoItemPriority Low = new() { Name = nameof(Low) };
        public static readonly TodoItemPriority Medium = new() { Name = nameof(Medium) };
        public static readonly TodoItemPriority High = new() { Name = nameof(High) };
    }
}