using System.Threading;
using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace todo_app_test
{
    public class MockHiLoStore : MockWithReturnValue<int>, IHiLoStore
    {
        public Task<int> NextLowAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ReturnValue);
        }
    }
}
