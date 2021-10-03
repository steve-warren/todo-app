using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public interface IHiLoRepository
    {
        Task<int> NextLowAsync(CancellationToken cancellationToken);
    }
}