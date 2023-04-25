using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace todo_app_test;

public class HiLoTests
{
    [Fact]
    public async Task HiLo_Should_Increment_By_1()
    {
        var state = new HiLoState(blockSize: 1);
        var store = new MockHiLoStore();

        store.SetValue(1);
        (await state.NextIdAsync(store, CancellationToken.None)).Should().Be(1);

        store.SetValue(2);
        (await state.NextIdAsync(store, CancellationToken.None)).Should().Be(2);
    }

    [Fact]
    public async Task HiLo_Should_Call_Store_When_Depleted()
    {
        var state = new HiLoState(blockSize: 2);
        var store = new MockHiLoStore();

        store.SetValue(1);
        (await state.NextIdAsync(store, CancellationToken.None)).Should().Be(1);
        (await state.NextIdAsync(store, CancellationToken.None)).Should().Be(2);

        store.SetValue(3);
        (await state.NextIdAsync(store, CancellationToken.None)).Should().Be(3);
    }
}
