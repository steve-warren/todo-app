
namespace WarrenSoftware.TodoApp.Core.Domain
{
    public abstract record Enumeration
    {
        public string Name { get; init; } = "";

        public override string ToString() => Name;
    }
}