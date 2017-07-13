using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _8Chapter
{
    /// <summary>
    /// 测试静态字段初始化的执行顺序，以推到出静态的Singleton实现方法
    /// 静态字段会在静态类任何静态方法或静态字段调用之前进行初始化
    /// </summary>
    class BeforeFieldInit
    {

        public readonly static string x = EchoAndReturn("In type initializer");

        static BeforeFieldInit()
        {
            Console.WriteLine("static beforefieldinit...");
        }

        public static string EchoAndReturn(string str)
        {
            Console.WriteLine(str);
            return str;
        }

    }

    public class BeforeFieldInitTest
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Test()
        {
            Console.WriteLine("Starting Main");
            BeforeFieldInit.EchoAndReturn("Echo!");
            Console.WriteLine("After echo");

            string y = BeforeFieldInit.x;
            //Use the value just to avoid compiler cleverness
            if (y != null)
            {
                Console.WriteLine("After field access");
            }

            Console.ReadLine();
        }
    }
}
