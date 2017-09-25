using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            //SyncDemo demo = new SyncDemo();
            //demo.Run();

            //AsyncDemo demo2 = new AsyncDemo();
            //demo2.Run();

            //CancelTaskDemo demo3 = new CancelTaskDemo();
            //demo3.Run();


            //TaskWaitDemo demo4 = new TaskWaitDemo();
            //demo4.Run();

            //TaskWhenAllDemo demo = new TaskWhenAllDemo();
            //demo.Run();

            TaskDelayDemo demo = new TaskDelayDemo();
            demo.Run();

            Console.ReadLine();
        }
    }
}
