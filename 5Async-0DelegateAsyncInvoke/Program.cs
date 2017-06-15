using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5Async_0DelegateAsyncInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            //SyncInvoke();

            //Test test = new Test();
            //test.test();

            AsyncInvoke();
            Console.ReadLine();
        }
        static void SyncInvoke()
        {
            Console.WriteLine("Client Application start!");
            Thread.CurrentThread.Name = "Main Thread";

            Calculator cal = new Calculator();
            int result = cal.Add(5, 2);
            Console.WriteLine("Result:{0}", result);

            for (int i = 1; i <= 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0} :Client excuted {1} seconds", Thread.CurrentThread.Name, i);
            }

            Console.WriteLine("Press any key to exit...");

        }

        public delegate int AddDelegate(int x,int y);
        static void AsyncInvoke()
        {

            Console.WriteLine("Client Application start!");
            Thread.CurrentThread.Name = "Main Thread";

            Calculator cal = new Calculator();

            AddDelegate addDel = cal.Add;

            string data = "any data you want to pass";

            IAsyncResult asyncResult = addDel.BeginInvoke(2, 5, OnAddComplete, data);

          

            for (int i = 1; i <= 1; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0} :Client excuted {1} seconds", Thread.CurrentThread.Name, i);
            }

            Console.WriteLine("Client Application end!");

            //int result = addDel.EndInvoke(asyncResult);

            //Console.WriteLine("Result:{0}", result);

            //Console.WriteLine("Press any key to exit...");
        }
        //回调方法：异步模式无法根本解决阻塞主线程问题（  如果在主线程中调用 endinvoke 可能会阻塞主线程），所以通过回调函数 把后续任务交给回调函数 从而腾出主线程
        static void OnAddComplete(IAsyncResult asyncResult)
        {
            AsyncResult result = (AsyncResult)asyncResult;
            AddDelegate del = (AddDelegate)result.AsyncDelegate;
            int addresult = del.EndInvoke(asyncResult);
            string data = (string)result.AsyncState;

            Console.WriteLine("Result:{0}", addresult);
            Console.WriteLine(data);

        }
    }


    //同步调用
    public class Calculator
    {
        public int Add(int x, int y)
        {
            if (Thread.CurrentThread.IsThreadPoolThread)
                Thread.CurrentThread.Name = "Pool Thread";

            Console.WriteLine("method Invoked!");

            for (int i = 1; i <= 2; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0} :Add excuted {1} seconds", Thread.CurrentThread.Name, i);
            }

            Console.WriteLine("method completed!");
            return x + y;
        }
    }
}
