
using System;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Tests.Core
{
    public class MockSystemClock : ISystemClock
    {
        public DateTimeOffset Time { get; init; }
        public DateTimeOffset Now() => Time;
    }

    public static class TimeConstants
    {
        public static readonly DateTimeOffset DateAndTime = new(2021, 09, 30, 23, 06, 0, TimeSpan.FromHours(-5));
    }
}