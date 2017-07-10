using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5Anonymous
{
    class Program
    {
        static void Main(string[] args)
        {

            Test1();

            Console.ReadLine();

        }
        /// <summary>
        /// 被匿名方法捕获的确实是变量，而不是委托创建时候变量的值
        /// 整个方法中使用的是同一个变量
        /// </summary>
        static void Test()
        {
            string captured = "before x is created";

            MethodInvoker x = delegate
            {
                Console.WriteLine(captured);
                captured = "Changed by x";
            };

            captured = "directly before x  is invoke";
            x();

            Console.WriteLine(captured);

            captured = "before second invoke";
            x();

            Console.WriteLine(captured);
        }

        /// <summary>
        /// 当一个变量被捕获时，捕捉的是变量的引用（C++中既有按值捕获，也有按引用捕获）
        /// </summary>
        static void Test1()
        {
            List<MethodInvoker> list = new List<MethodInvoker>();

            for (int index=0;index<5;index++)
            {
                //实例化Counter
                //在循环内部，同一个变量声明会引用不同变量的实例
                int counter = index * 10;
                list.Add(delegate 
                {
                    //打印捕获的变量
                    Console.WriteLine(counter);
                    counter++;
                });
            }

            foreach (MethodInvoker t in list)
            {
                t();
            }

            list[0]();
            list[0]();
            list[0]();

            list[1]();
            list[1]();
        }

        static void Test2()
        {
            MethodInvoker[] delegates = new MethodInvoker[2];

            //共享变量（只声明了一次）
            int outside = 0;

            for (int i = 0; i < 2; i++)
            {
                //非共享变量（每次循环都会实例化一个新的inside变量）
                int inside = 0;
                delegates[i] = delegate
                {
                    Console.WriteLine("{0}:{1}", inside, outside);
                    inside++;
                    outside++;
                };
            }

            MethodInvoker first = delegates[0];
            MethodInvoker second = delegates[1];

            first();
            first();
            first();

            second();
            second();
        }
    }
}
