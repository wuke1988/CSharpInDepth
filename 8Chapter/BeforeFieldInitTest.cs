using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Chapter
{
    /// <summary>
    /// 测试静态字段初始化的执行顺序，以推到出静态的Singleton实现方法
    /// </summary>
    class BeforeFieldInitTest
    {

        class Test
        {
            public static string x = EchoAndReturn("In type initializer");

            public static string EchoAndReturn(string x)
            {
                Console.WriteLine(x);
                return x;
            }
        }

    }

    class BeforeFieldInitTest1
    {

        class Test
        {
            public static string x = EchoAndReturn("In type initializer");

            static Test()
            { }

            public static string EchoAndReturn(string x)
            {
                Console.WriteLine(x);
                return x;
            }
        }

    }
}
