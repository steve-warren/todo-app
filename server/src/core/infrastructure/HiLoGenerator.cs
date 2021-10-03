using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class HiLoGenerator
    {
        private readonly IHiLoRepository _repository;
        private readonly HiLoGeneratorState _state;

        public HiLoGenerator(IHiLoRepository repository, HiLoGeneratorState state)
        {
            _repository = repository;
            _state = state;
        }

        public Task<int> NextAsync(CancellationToken cancellationToken)
        {
            return _state.NextIdAsync(_repository, cancellationToken);
        }
    }
}