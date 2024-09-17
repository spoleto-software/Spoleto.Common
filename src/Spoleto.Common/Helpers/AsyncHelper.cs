using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spoleto.Common.Helpers
{
    /// <summary>
    /// Helper for async methods.
    /// </summary>
    public static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        /// <summary>
        /// Call asynchronous method from synchronous method.
        /// </summary>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Call asynchronous method from synchronous method.
        /// </summary>
        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}