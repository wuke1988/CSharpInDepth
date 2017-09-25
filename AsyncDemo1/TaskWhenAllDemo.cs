using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    /// <summary>
    /// 异步方法中异步等待任务
    /// </summary>
    class TaskWhenAllDemo
    {
        static int time=0;
        public void Run()
        {
            Task<int> task = GetRandomAsync();

            Console.WriteLine($"task.{nameof(task.IsCompleted)}:{task.IsCompleted}");
            Console.WriteLine($"task.{nameof(task.Result)}:{task.Result}");

            Console.ReadLine();
        }

        public async Task<int> GetRandomAsync()
        {
            time++;
            Task<int> t1 = Task.Run(
                ()=> {
                    Thread.Sleep(time*100);
                    return new Random().Next(100);
                }
                );
            time++;
            Console.WriteLine($"    t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
            Task<int> t2 = Task.Run(
                () => {
                    Thread.Sleep(time * 100);
                    return new Random().Next(100);
                }
                );
              await Task.WhenAll<int>(new Task<int>[] { t1,t2 });

            Console.WriteLine($"    t1.{nameof(t1.IsCompleted)}: {t1.IsCompleted}");
            Console.WriteLine($"    t2.{nameof(t2.IsCompleted)}: {t2.IsCompleted}");

            return t1.Result + t2.Result;
        }
    }
}
