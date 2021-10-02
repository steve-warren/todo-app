using System.Threading;
using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class InMemoryTodoItemIdentityService : ITodoItemIdentityService
    {
        public Task<int> NextIdAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}