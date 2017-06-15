using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Async_2
{
    class Program
    {
        static void Main(string[] args)
        {

            //MainAsync();
            Func<Task> lambda = async () => await Task.Delay(100);

            Func<Task<int>> lambda2 = async () => { await Task.Delay(100); return 10; };

            Task task = ThrowCancellationException();
            Console.WriteLine(task.Status);
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            Console.ReadLine();
        }

        static async Task ThrowCancellationException()
        {
            throw new OperationCanceledException();
        }

        static async Task MainAsync()
        {
            Task<int> task = ComputeLengthAsync(null);
            //task.Wait();
            Console.WriteLine(  task.Status);
            Console.WriteLine("Fetched the task");
            try
            {
                int length = await task;
                Console.WriteLine(length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task<int> ComputeLengthAsync(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            await Task.Delay(5000);
            return text.Length;
        }
    }
}
