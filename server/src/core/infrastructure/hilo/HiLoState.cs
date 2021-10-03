using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public sealed class HiLoState : IDisposable
    {
        private readonly SemaphoreSlim _semaphore = new(initialCount: 1);
        private readonly  int _blockSize;
        private HiLoValue _currentValue = new(Low: 0, High: 1);

        public HiLoState(int blockSize)
        {
            if (blockSize <= 0) throw new ArgumentOutOfRangeException(nameof(blockSize));

            _blockSize = blockSize;
        }

        public void Dispose()
        {
            _semaphore.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> NextIdAsync(IHiLoStore store, CancellationToken cancellationToken)
        {
            var nextValue = NextValue();

            while(nextValue.HasExceeded())
            {
                await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    if (nextValue.High == _currentValue.High)
                    {
                        var nextLow = await store.NextLowAsync(cancellationToken).ConfigureAwait(false);
                        _currentValue = new HiLoValue(nextLow, nextLow + _blockSize);
                    }

                    else
                    {
                        nextValue = NextValue();
                    }
                }

                finally
                {
                    _semaphore.Release();
                }
            }

            return nextValue.Low;
        }

        private HiLoValue NextValue()
        {
            HiLoValue current;
            HiLoValue next;

            do
            {
                current = _currentValue;
                next = current.NextValue();
            }
            while(Interlocked.CompareExchange(location1: ref _currentValue, value: next, comparand: current) != current);

            return next;
        }
    }
}