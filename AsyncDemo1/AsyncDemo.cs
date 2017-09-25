using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    /// <summary>
    /// 对SyncDemo进行优化
    /// 可见 :所有的工作都是在主线程中完成的，没有创建新的线程
    /// </summary>
    class AsyncDemo
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();
        
        public void Run()
        {
            stopwatch.Start();

            string url1 = "http://www.cnblogs.com/";
            string url2 = "http://www.cnblogs.com/liqingwen/";

            //保存结果的对象
            Task<int> result1 = CountCharacters("id1", url1);
            Task<int> result2 = CountCharacters("id2", url2);

            //ExtraOperation方法的执行 实在调用CountCharactersAsync时 等待的过程中执行的
            for (int i = 0; i < 3; i++)
            {
                ExtraOperation(i);
            }
            //等待Task<int>的结果，如果没有执行完将阻塞程序
            Console.WriteLine($"{url1} 字符个数 {result1.Result}");
            Console.WriteLine($"{url2} 字符个数 {result2.Result}");

            Console.ReadLine();
        }
        /// <summary>
        /// 异步方法：被调用后会立即返回，待异步操作完成后 中断主线程当下操作来优先执行异步方法剩余操作
        /// 异步方法 到达结尾或遇到return时 根据返回类型分为三类：
        /// a.void 退出控制流
        /// b.Task 设置 Task 的属性并退出
        /// c.Task<T> 设置 Task 的属性以及result值 并退出
        /// 异步方法结束后，调用方法继续执行，并从异步方法获取到 task对象；需要读取task值时，会暂停知道task被赋值后
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<int> CountCharacters(string id, string url)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"{id} CountCharacters 方法开始 时间: {stopwatch.ElapsedMilliseconds} ms");

                string result = await client.DownloadStringTaskAsync(new Uri(url));               
                Console.WriteLine($"{id} CountCharacters 方法结束 时间: {stopwatch.ElapsedMilliseconds} ms");

                return result.Length;
            }
        }
        public void ExtraOperation(int id)
        {
            string str = "str";
            for (int i = 0; i < 10000; i++)
            {
                str += i;
            }
            Console.WriteLine($"{id} ExtraOperation方法完成 时间:{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
