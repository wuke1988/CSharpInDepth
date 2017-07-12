using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6Yeild
{
   public  class YeildDemo
    {
        static readonly string Padding = new string(' ', 30);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static IEnumerable<int> CreateEnumerable()
        {
            Console.WriteLine("{0} Start of CreateEnumerable()", Padding);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("{0} About to yeild {1}", Padding, i);
                yield return i;
                Console.WriteLine("{0} After yeild", Padding);
            }
            Console.WriteLine("{0} Yeild final value", Padding);
            //代码不会在最后一个yeild return 处结束
            yield return -1;
            //再次调用movenext时执行后续的语句
            Console.WriteLine("{0} End of CreateEnumerable", Padding);

        }


        public static void Test()
        {
            //在第一次调用MoveNext之前，CreateEnumerable()不会调用
            IEnumerable<int> iterable = CreateEnumerable();
            IEnumerator<int> iterator = iterable.GetEnumerator();

            while (true)
            {
                Console.WriteLine("Calling Movenext()...");
               //所有工作在调用Movenext时就完成了（调用movenext时，执行代码完 yeild return 停止），获取Current的值不会执行任何代码
                bool result = iterator.MoveNext();

                Console.WriteLine("... Movenext result={0}",result);
                if (!result)
                {
                    break;
                }
                Console.WriteLine("...Fetching Current...");
                Console.WriteLine("...Current result={0}",iterator.Current);

            }
        }
    }
}
