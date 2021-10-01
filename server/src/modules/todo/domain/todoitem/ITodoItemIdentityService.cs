using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface ITodoItemIdentityService
    {
        Task<int> NextIdAsync(CancellationToken cancellationToken);
    }
}