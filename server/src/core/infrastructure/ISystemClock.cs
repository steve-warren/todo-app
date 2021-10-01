
using System;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public interface ISystemClock
    {
        DateTimeOffset Now();
    }

    public sealed class DefaultSystemClock : ISystemClock
    {
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}