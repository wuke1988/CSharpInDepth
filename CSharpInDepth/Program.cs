﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            //CountingEnumerable enumerable = new CountingEnumerable();
            //foreach (int a in enumerable)
            //{
            //    Console.WriteLine(a);
            //}

            //Reflect.Test();




            //CovarianceDemo demo = new CovarianceDemo();
            //demo.Demo();

            ContravarianceDemo demo = new ContravarianceDemo();
            demo.Demo();

            Console.ReadLine();
        }
    }
}
