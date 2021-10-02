using System.Threading;
using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}