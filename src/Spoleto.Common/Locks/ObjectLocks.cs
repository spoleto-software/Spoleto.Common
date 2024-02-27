using System.Collections.Generic;

namespace Spoleto.Common.Locks
{
    /// <summary>
    /// The static class for separate locks for objects.
    /// </summary>
    /// <remarks>
    /// Example:
    /// object lockObj = ObjectLocks.GetObjectLock(lockObjetKey);<br/>
    /// lock (lockObj)
    /// {<br/>
    ///     try<br/>
    ///     {<br/>
    ///        // code<br/>
    ///     }<br/>
    ///     finally<br/>
    ///     {<br/>
    ///        ObjectLocks.ReleaseObjectLock(lockObjetKey);<br/>
    ///     }<br/>
    /// }<br/>
    /// </remarks>
    public static class ObjectLocks
    {
        /// <summary>
        /// The class of locks with deep.
        /// </summary>
        private class IntLock
        {
            /// <summary>
            /// Locks deep.
            /// </summary>
            public int Deep { get; set; }

            /// <summary>
            /// Return text representation.
            /// </summary>
            /// <returns></returns>
            public override string ToString() => $"Deep = {Deep}";
        }


        private static readonly Dictionary<string, IntLock> ObjectLocksDic = new Dictionary<string, IntLock>();
        private static readonly object CacheLock = new object();

        /// <summary>
        /// Returns lock for object.
        /// </summary>
        public static object GetObjectLock(string key)
        {
            lock (CacheLock)
            {
                if (!ObjectLocksDic.TryGetValue(key, out var cachedItem))
                {
                    cachedItem = new IntLock();
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