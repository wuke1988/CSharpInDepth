using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5Async_3
{
    class Program
    {
        static string Greeting(string name)
        {
            Thread.Sleep(3000);

            return string.Format("Hello ,{0}",name);
        }
        static Task<string> GreetingAsync(string name)
        {
            string str = "Test";
            Func<string, int> func1 = (string x) => { return (x+str).Length; };
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            return Task.Run( ()=> Greeting(name));            
        }
        private static async void CallerWithAsync()
        {
            string result = await GreetingAsync("wuke");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(result);
        }
        private static void CallerWithContinuationBack()
        {
            Task<string> t1 = GreetingAsync("wuke");

            Action<Task<string>> action = (t => { Console.WriteLine(t.Result); });

            t1.ContinueWith(action);
        }
        private static async void MutipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("wuke");
            Task<string> t2 = GreetingAsync("guohui");
            await Task.WhenAll(t1,t2);
            Console.WriteLine(t1.Result);
            Console.WriteLine(t2.Result);

        }
        private static Func<string, string> greetingInvoker = Greeting;

        private static AsyncCallback callback1 = ar => { if (ar.IsCompleted == true) Console.WriteLine("async call OK"); };
        static IAsyncResult BeginGreeting(string name,AsyncCallback callback,object state)
        {
            return greetingInvoker.BeginInvoke(name,callback,state);
        }
    
        static string EndGreeting(IAsyncResult result)
        {
            return greetingInvoker.EndInvoke(result);
        }
        private static async void ConvertAsyncPattern()
        {
            string result = await  Task<string>.Factory.FromAsync<string>(BeginGreeting,EndGreeting,"wuke",null);

            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            //CallerWithAsync();
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //MutipleAsyncMethodsWithCombinators1();

            ConvertAsyncPattern();
            Console.ReadLine();
        }
    }
}
