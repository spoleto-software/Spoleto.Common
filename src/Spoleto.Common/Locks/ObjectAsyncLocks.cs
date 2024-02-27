using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spoleto.Common.Locks
{
    /// <summary>
    /// The static class for separate async locks for objects.
    /// </summary>
    /// <remarks>
    /// Example:
    /// var lockObject = ObjectAsyncLocks.GetObjectLock(lockObjetKey);<br/>
    /// using (await lockObject.LockAsync())<br/>
    /// {<br/>
    ///     try<br/>
    ///     {<br/>
    ///        // code<br/>
    ///     }<br/>
    ///     finally<br/>
    ///     {<br/>
    ///        ObjectAsyncLocks.ReleaseObjectLock(lockObjetKey);<br/>
    ///     }<br/>
    /// }<br/>
    /// </remarks>
    public static class ObjectAsyncLocks
    {
        /// <summary>
        /// The class of async locks with deep.
        /// </summary>
        public class AsyncIntLock : IDisposable
        {
            /// <summary>
            /// The async lock object.
            /// </summary>
            private readonly AsyncLock _asyncLock;

            /// <summary>
            /// Default constructor.
            /// </summary>
            public AsyncIntLock()
            {
                _asyncLock = new AsyncLock();
            }

            /// <summary>
            /// Locks deep.
            /// </summary>
            public int Deep { get; set; }

            /// <summary>
            /// Releases the lock.
            /// </summary>
            public void Dispose() => _asyncLock.Dispose();

            /// <summary>
            /// Creates the lock.
            /// </summary>
            public async Task<AsyncIntLock> LockAsync()
            {
                await _asyncLock.LockAsync().ConfigureAwait(false);
                return this;
            }

            /// <summary>
            /// Return text representation.
            /// </summary>
            /// <returns></returns>
            public override string ToString() => $"Deep = {Deep}";
        }


        private static readonly Dictionary<string, AsyncIntLock> ObjectLocksDic = new();
        private static readonly object CacheLock = new();

        /// <summary>
        /// Returns lock for object.
        /// </summary>
        public static AsyncIntLock GetObjectLock(string key)
        {
            lock (CacheLock)
            {
                if (!ObjectLocksDic.TryGetValue(key, out var cachedItem))
                {
                    cachedItem = new AsyncIntLock();
                    ObjectLocksDic.Add(key, cachedItem);
                }

                cachedItem.Deep++;
                return cachedItem;
            }
        }

        /// <summary>
        /// Release object.
        /// </summary>
        public static void ReleaseObjectLock(string key)
        {
            lock (CacheLock)
            {
                if (ObjectLocksDic.TryGetValue(key, out var cachedItem))
                {
                    cachedItem.Deep--;
                    if (cachedItem.Deep == 0)
                        ObjectLocksDic.Remove(key);
                }
            }
        }
    }
}