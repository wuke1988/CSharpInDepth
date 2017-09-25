using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    /// <summary>
    /// 在调用方法中同步等待任务:Task::WaitAll()  WaitAny()
    /// </summary>
    public class TaskWaitDemo
    {
        public void Run()
        {
            Task<int> task = CountCharactersAsync("http://www.cnblogs.com/liqingwen/");

            //task.Wait();
            //如果不加task.Wait() 主线程会直接执行task.IsCompleted
            Console.WriteLine($"{nameof(task.IsCompleted)}:{task.IsCompleted}");
            //不论加不加task.Wait()， 主线程执行 task.Result 都会等待 task 完成
            Console.WriteLine($"Result is {task.Result}");

            Console.ReadLine();
        }
        public void Run2()
        {
            Task<int> task1 = CountCharactersAsync("http://www.cnblogs.com/liqingwen/");
            Task<int> task2 = CountCharactersAsync("http://www.cnblogs.com/liqingwen/");

            Task.WaitAll(new Task[2] { task1,task2});
            Task.WaitAny(new Task[2] { task1,task2});


            Console.WriteLine($"task1.{nameof(task1.IsCompleted)}: {task1.IsCompleted}");
            Console.WriteLine($"task2.{nameof(task2.IsCompleted)}: {task2.IsCompleted}");

            Console.ReadLine();
        }
        
        
        /// <summary>
        /// 统计字符数量
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<int> CountCharactersAsync(string address)
        {
            var result = await Task.Run(() => new WebClient().DownloadStringTaskAsync(address));
            return result.Length;
        }

    }

    
}
