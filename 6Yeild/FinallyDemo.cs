using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _6Yeild
{
    public class FinallyDemo
    {
        static IEnumerable<int> CountWithTimeLimit(DateTime limit)
        {
            try
            {
                for (int i = 0; i <= 1000; i++)
                {
                    if (DateTime.Now >= limit)
                    {
                        yield break;
                        //Console.WriteLine(" yield break End");
                    }
                    yield return i;
                }
            }
            finally
            {
                Console.WriteLine("STOPPING!");
            }
        }

        public static void Test()
        {
            DateTime stop = DateTime.Now.AddSeconds(2);

            foreach (int i in CountWithTimeLimit(stop))
            {
                Console.WriteLine("Received {0}",i);
                Thread.Sleep(300);
            }
        }

        public static void Test1()
        {
            DateTime stop = DateTime.Now.AddSeconds(2);

            //foreach结束的时候，会调用IEnumerator的Dispose()方法
            //调用Dispose()方法，会自动调用 finally方法
            foreach (int i in CountWithTimeLimit(stop))
            {
                Console.WriteLine("Received {0}", i);
                if(i>=3)
                    return;
                Thread.Sleep(300);
            }
        }
    }
}
