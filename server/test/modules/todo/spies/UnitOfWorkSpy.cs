using System.Threading.Tasks;
using System.Threading;
using WarrenSoftware.TodoApp.Core.Domain;

namespace todo_app_test
{
    public class UnitOfWorkSpy : Spy, IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            WasCalled = true;
            return Task.CompletedTask;
        }
    }
}
