using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace todo_app_test
{
    public class MockTodoItemIdentityService : MockWithReturnValue<int>, ITodoItemIdentityService
    {
        public MockTodoItemIdentityService(int id) => ReturnValue = id;

        public Task<int> NextIdAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ReturnValue);
        }
    }
}
