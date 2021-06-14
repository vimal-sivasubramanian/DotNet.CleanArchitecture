using System.Threading.Tasks;

namespace DotNet.EventSourcing.Core
{
    public static class TaskExtensions
    {
        /// <summary>
        /// This method should be prefered over Task.Result, to avoid possible deadlocks.
        ///
        /// The default Task.Result will "virtually" block the current thread until a result is ready to consume (or an exception occured), but the actual thread is returned to the threadpool, freeing it to work on other tasks.
        /// Another effect is, that when the result is ready to consume, the default is to resume execution on the same thread that originally spawned the async operation.
        /// This can lead to a subtile deadlock, where the thread that we want to resume on, in the meantime have been blocked by another operation.
        /// It is therefore good practice to always use ConfigureAwait(false) if it's not critical important that we return on the same thread. This way, another thread can take over and resume the execution.
        /// The history says that this default behaviour is due to WinForms and WPF applications, as they are mainly spawning async task on the UI thread - so it made good sense to return to the same to avoid UI threading problems.
        /// </summary>
        public static T SafeResult<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
