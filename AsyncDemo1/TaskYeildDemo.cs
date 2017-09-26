using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    class TaskYeildDemo
    {
        static int count =0;
        public void Run()
        {
            int num = 1000000;
            Task<int> task = Yield1000(num);

            Loop(num);
            Loop(num);
            Loop(num);

            Console.WriteLine($"task.Result:{task.Result}");
            Console.ReadLine();
        }
        public void Loop(int num)
        {

            Console.WriteLine($"Loop {count}  is Run");

            for (var i = 0; i < num; i++) ;

            count++;

            Console.WriteLine($"Loop {count}  is End!");
        }

        public async Task<int> Yield1000(int n)
        {
            int sum = 0;
            for (int i = 1; i < n; i++)
            {
                sum += i;
                if (i % 100000 == 0)
                {
                    Console.WriteLine($"Yield1000 is run, i={i} ");
                    await Task.Yield();
                }
            }
            return sum;
        }
    }
   
}
