using System;

namespace WarrenSoftware.TodoApp.Core.Domain
{
    public record ArchiveState : Enumeration
    {
        public static ArchiveState Parse(string name)
        {
            return name switch
            {
                nameof(NotArchived) => NotArchived,
                nameof(Archived) => Archived,
                _ => throw new ArgumentException("invalid name", nameof(name))
            };
        }

        public static readonly ArchiveState NotArchived = new() { Name = nameof(NotArchived) };
        public static readonly ArchiveState Archived = new() { Name = nameof(Archived) };
    }
}