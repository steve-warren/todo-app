using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Domain
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}