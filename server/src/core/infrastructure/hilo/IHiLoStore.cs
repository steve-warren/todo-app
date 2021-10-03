using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public interface IHiLoStore
    {
        Task<int> NextLowAsync(CancellationToken cancellationToken);
    }
}