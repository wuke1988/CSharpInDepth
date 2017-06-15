using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Error
{
    class Program
    {
        private static async Task ThrowException(int ms,string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);
        }
        private static async Task HandleException()
        {
            try
            {
               await ThrowException(2000, "Test");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //并行运行两个任务，只能捕获第一个的异常
        private static async void StartTwoTaskParralle()
        {
            try
            {
                Task t1 = ThrowException(2000,"Error1");
                Task t2 = ThrowException(2000, "Error2");

                await  Task.WhenAll(t1, t2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //可通过获取任务对象的Exception.InnerException来获取多个任务的异常
        private static async void StartTwoTaskParralle2()
        {
            Task t1 = ThrowException(2000, "Error1");
            Task t2 = ThrowException(2000, "Error2");
            try
            {
      
                await Task.WhenAll(t1, t2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (t2.IsFaulted == true)
                {
                    Console.WriteLine(t2.Exception.InnerException.Message);
                }
            }
        }
        //将并发的任务运行结果写到一个Task中，通过这个Task的innerexceptions来获取所有任务的异常
        private static async void StartTwoTaskParralle3()
        {
            Task task=null;
            try
            {
                Task t1 = ThrowException(2000, "Error1");
                Task t2 = ThrowException(2000, "Error2");

                task= Task.WhenAll(t1, t2);
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                foreach (var exp1 in task.Exception.InnerExceptions)
                {
                    Console.WriteLine(exp1.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            // HandleException();

            //StartTwoTaskParralle();


            //StartTwoTaskParralle2();


            StartTwoTaskParralle3();
            Console.ReadLine();
        }
    }
}
