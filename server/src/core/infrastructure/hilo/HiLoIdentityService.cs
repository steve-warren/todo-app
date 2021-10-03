using System.Threading;
using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class HiLoIdentityService : IIdentityService
    {
        private readonly IHiLoStore _store;
        private readonly HiLoState _state;

        public HiLoIdentityService(IHiLoStore store, HiLoState state)
        {
            _store = store;
            _state = state;
        }

        public Task<int> NextIdAsync(CancellationToken cancellationToken)
        {
            return _state.NextIdAsync(_store, cancellationToken);
        }
    }
}