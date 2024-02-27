using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spoleto.Common.Locks
{
    /// <summary>
    /// The special lock for async body.
    /// </summary>
    public class AsyncLock : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        /// <summary>
        /// Creates the lock.
        /// </summary>
        /// <returns></returns>
        public async Task<AsyncLock> LockAsync()
        {
            await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
            return this;
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        public void Dispose()
        {
            _semaphoreSlim.Release();
        }
    }
}
