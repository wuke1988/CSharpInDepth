using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    class Program
    {
        static void Main(string[] args)
        {
            //DynamicDemo demo = new DynamicDemo();
            //demo.Demo1();
            //demo.Demo2();
            //demo.Demo3();

            //PythonScopeDemo demo = new PythonScopeDemo();
            //demo.Demo1();

            //DynamicUsageDemo.Demo1();
            //DynamicUsageDemo.Demo2();
            //DynamicUsageDemo.Demo3();

            //ExpandoObjectDemo demo = new ExpandoObjectDemo();
            //demo.Demo1();

            ExpandoObjectDemo2 demo = new ExpandoObjectDemo2();
            demo.Demo1();

            DynamicObjectDemoTest demo1 = new DynamicObjectDemoTest();
            demo1.Demo1();
            demo1.Demo2();
            demo1.Demo3();

            DynamicObjectDemo2 demo2 = new DynamicObjectDemo2();
            demo2.Demo1();
            

            Console.ReadLine();
        }
    }
}
