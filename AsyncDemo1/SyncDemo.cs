using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    public class SyncDemo
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();
        public void Run()
        {
            stopwatch.Start();

            string url1 = "http://www.cnblogs.com/";
            string url2 = "http://www.cnblogs.com/liqingwen/";

            var result1 = CountCharacters("id1",url1);
            var result2 = CountCharacters("id2",url2);

            for (int i = 0; i < 3; i++)
            {
                ExtraOperation(i);
            }
            Console.WriteLine($"{url1} 字符个数 {result1}");
            Console.WriteLine($"{url2} 字符个数 {result2}");

            Console.ReadLine();
        }
        public int CountCharacters(string id, string url)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"{id} CountCharacters 方法开始 时间: {stopwatch.ElapsedMilliseconds} ms");
                string text = client.DownloadString(url);
                Console.WriteLine($"{id} CountCharacters 方法结束 时间: {stopwatch.ElapsedMilliseconds} ms");
                return text.Length;
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
