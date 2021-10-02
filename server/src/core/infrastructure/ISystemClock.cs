using System;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public interface ISystemClock
    {
        DateTimeOffset Now();
    }
}