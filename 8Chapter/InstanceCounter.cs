using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Chapter
{
    class InstanceCounter
    {
        public static void Demo()
        {
            Person person = new Person("Wuke", 30);
            Person person1 = new Person("GuoHui", 26);

            Console.WriteLine("Person Class Instance Count:{0}",person.Counter);
        }
    }

    /// <summary>
    /// 记录类实例化对象的个数
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        private static int counter=0;

        public int Counter { get { return counter; } }

        //readonly + static 静态只读，可以通过静态构造函数来赋初值
        private static readonly object countLock = new object();

        private Person()
        {
        }

        public Person(string Name,int Age)
        {
            this.Age = Age;
            this.Name = Name;

            lock(countLock)
            {
                counter++;
            }

        }

    }
}
