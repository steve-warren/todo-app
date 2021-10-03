using System.Threading.Tasks;
using System.Threading;
using WarrenSoftware.TodoApp.Core.Domain;

namespace todo_app_test
{
    public class MockTodoItemIdentityService : MockWithReturnValue<int>, IIdentityService
    {
        public MockTodoItemIdentityService(int id) => ReturnValue = id;

        public Task<int> NextIdAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ReturnValue);
        }
    }
}
