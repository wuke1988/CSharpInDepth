using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _5Async
{
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }

    public interface IAwaiterable<T>
    {
        IAwaiter<T> GetAwaiter();
    }

    public class AggregatedExceptionAwaiter
    {
        private Task task;
        public AggregatedExceptionAwaiter(Task task)
        {
            this.task = task;
        }
        public bool IsCompleted
        {
            get { return task.GetAwaiter().IsCompleted; }
        }
        public void OnCompleted(Action continuation)
        {
            task.GetAwaiter().OnCompleted(continuation);
        }
        public void GetResult()
        {
            task.Wait();
        }
    }

    public class AggregatedExceptionAwaiterable
    {
        public Task task;
        public AggregatedExceptionAwaiter GetAwaiter()
        {
            return new AggregatedExceptionAwaiter(task);
        }
    }
}
