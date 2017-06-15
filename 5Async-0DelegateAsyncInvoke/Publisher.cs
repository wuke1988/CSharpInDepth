using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5Async_0DelegateAsyncInvoke
{
    public class Test
    {
        public static object[] FireEvent(Delegate dels, params object[] args)
        {
            List<object> list = new List<object>();

            var delList = dels.GetInvocationList();

            foreach (Delegate del in delList)
            {
                list.Add(del.DynamicInvoke(args));
            }
            return list.ToArray();
        }

        public void test()
        {
            Publisher publisher = new Publisher();

            Subscriber1 sub1 = new Subscriber1();
            Subscriber2 sub2 = new Subscriber2();

            publisher.myEvent += sub1.OnEvnet;
            publisher.myEvent += sub2.OnEvnet;

            publisher.DosomeThing1();

            Console.WriteLine();
        }            
    }
    public class Publisher
    {
        public event EventHandler myEvent;

        public void DoSomeThing()
        {
            if (myEvent != null)
            {      
                Test.FireEvent(myEvent,this, EventArgs.Empty);
                Console.WriteLine("Publisher DoSomeThing...");
            }
        }
        public void DosomeThing1()
        {

            if (myEvent != null)
            {
                Delegate[] delList = myEvent.GetInvocationList();
                foreach (Delegate del in delList)
                {
                    ((EventHandler)del).BeginInvoke(this,EventArgs.Empty,null,null);
                }
                    Console.WriteLine("Publisher DoSomeThing...");
            }

        }
    }
    public class Subscriber1
    {
        public void OnEvnet(object sender, EventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine("Waited for 3 seconds, Subscriber1 invoked!");
        }
    }

    public class Subscriber2
    {
        public void OnEvnet(object sender, EventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Waited for 2 seconds, Subscriber2 invoked!");
        }
    }
}
