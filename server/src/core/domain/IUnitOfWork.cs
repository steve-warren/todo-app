using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Domain
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}