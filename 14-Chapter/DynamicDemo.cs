using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    public class DynamicDemo
    {
        /// <summary>
        /// dynamic类型可以多次改变其类型，而var不行
        /// </summary>
        public void Demo1()
        {
            dynamic value = 100;
            Console.WriteLine(++value);
            value = new List<String> { "ABC" };
            foreach (string str in value)
                Console.WriteLine(str);

            //var value1 = 100;
            //value1 = new List<string>();
        }
        public void Demo2()
        {
            dynamic valueToAdd = 2;
            dynamic items = new List<int> { 1, 2, 3 };
            foreach (dynamic item in items)
            {
                dynamic result = item + valueToAdd;

                //为什么 先把int相加 再转换为String就不行
                //String result1 = item + valueToAdd;
                Console.WriteLine(result);
            }
        }
        public void Demo3()
        {
            dynamic valueToAdd = 2;
            dynamic items = new List<String> { "First", "Second", "Third" };
            foreach (dynamic item in items)
            {
                //这里是把valueToAdd先转换为String就么
                string test = item + valueToAdd;
                Console.WriteLine(test);
            }
            Console.WriteLine(valueToAdd.GetType());
            
        }

    }
}
