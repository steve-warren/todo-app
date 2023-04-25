namespace WarrenSoftware.TodoApp.Core.Infrastructure;
public sealed class DefaultSystemClock : ISystemClock
{
    public DateTimeOffset Now() => DateTimeOffset.Now;
}
