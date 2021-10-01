
using System;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Tests.Core
{
    public class MockSystemClock : ISystemClock
    {
        public DateTimeOffset Time { get; init; }
        public DateTimeOffset Now() => Time;
    }
}