using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}