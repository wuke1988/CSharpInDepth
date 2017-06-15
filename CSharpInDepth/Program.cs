using System;
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
            CountingEnumerable enumerable = new CountingEnumerable();
            foreach (int a in enumerable)
            {
                Console.WriteLine(a);
            }
            Console.ReadLine();
        }
    }
}
