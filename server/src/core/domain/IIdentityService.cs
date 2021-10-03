using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Domain
{
    public interface IIdentityService
    {
        Task<int> NextIdAsync(CancellationToken cancellationToken);
    }
}