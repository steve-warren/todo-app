using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace todo_app_test;

public class UnitOfWorkSpy : Spy, ITodoUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        WasCalled = true;
        return Task.FromResult(1);
    }
}
