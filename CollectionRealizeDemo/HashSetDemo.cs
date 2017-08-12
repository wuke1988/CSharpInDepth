using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    public class HashSetDemo
    {
        /// <summary>
        /// HashSet 要求元素不能重复，自动去除掉重复值
        /// </summary>
        public void Demo1()
        {
            string[] names = new string[] {
                "mahesh",
                "vikram",
                "mahesh",
                "mayur",
                "suprotim",
                "saket",
                "manish"
            };

            HashSet<string> hashSet = new HashSet<string>(names);

            Console.WriteLine("Array Count "+names.Length);

            foreach (string name in names)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("HashSet Count "+hashSet.Count);

            foreach(string name in hashSet)
            {
                Console.WriteLine(name);
            }
        }
       static string[] names = new string[] {
    "Tejas", "Mahesh", "Ramesh", "Ram", "GundaRam", "Sabnis", "Leena",
    "Neema", "Sita" , "Tejas", "Mahesh", "Ramesh", "Ram",
    "GundaRam", "Sabnis", "Leena", "Neema", "Sita" ,
    "Tejas", "Mahesh", "Ramesh", "Ram", "GundaRam",
    "Sabnis", "Leena", "Neema", "Sita" , "Tejas",
    "Mahesh", "Ramesh", "Ram", "GundaRam", "Sabnis",
    "Leena", "Neema", "Sita",
    "Tejas", "Mahesh", "Ramesh", "Ram", "GundaRam", "Sabnis",
       ""};
        /// <summary>
        /// 测试List和HashSet的Add方法的速度：HashSet效率慢
        /// </summary>
        public void Demo2()
        {
            List<string> list = new List<string>();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string name in names)
            {
                list.Add(name);
            }
            watch.Stop();

            Console.WriteLine("List Add use "+watch.ElapsedMilliseconds+" ms");

            watch.Reset();
            watch.Start();
            HashSet<string> hashset = new HashSet<string>();
            foreach(string name in names)
            {
                hashset.Add(name);
            }
            watch.Stop();

            Console.WriteLine("HashSet Add use " + watch.ElapsedMilliseconds + " ms");
        }
        /// <summary>
        /// 但 Remove，Contain 函数，HashSet集合效率很高
        /// </summary>
        public void Demo3()
        {
            List<string> list = new List<string>();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string name in names)
            {
                list.Contains(name);
            }
            watch.Stop();

            Console.WriteLine("List Add use " + watch.ElapsedMilliseconds + " ms");

            watch.Reset();
            watch.Start();
            HashSet<string> hashset = new HashSet<string>();
            foreach (string name in names)
            {
                hashset.Contains(name);
            }
            watch.Stop();

            Console.WriteLine("HashSet Add use " + watch.ElapsedMilliseconds + " ms");
        }


    }
}
