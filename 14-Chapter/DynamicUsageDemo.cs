using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    public static class DynamicUsageDemo
    {
        private static bool AddConditionallyImp<T>(IList<T> list,T item) 
        {
            if (list.Count < 10)
            {
                list.Add(item);
                return true;
            }
            return false;           
        }
        /// <summary>
        /// 动态类型作用1：执行时直接推断并执行代码，省去通过反射获取方法 再执行
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddContionally(dynamic list, dynamic item)
        {
            return AddConditionallyImp(list,item);
        }
        /// <summary>
        /// 动态类型作用1：原始方法 需要根据参数类型通过反射来获取方法 再执行
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddContionally1(IEnumerable list, object item)
        {
            Type type = list.GetType();
            if (type == typeof(List<string>))
            {
                MethodInfo method = typeof(DynamicUsageDemo).GetMethod("AddConditionallyImp");

                
                object[] param = new object[] { list,item};
                //method.Invoke(obj:null,parameters: param);
            }
            return false;
        }
        /// <summary>
        /// 动态类型作用2：弥补泛型操作符的不足（泛型操作符无法直接与泛型操作）
        /// 但要注意：类型是否可以执行相加操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T DynamicSum<T>(this IEnumerable<T> source)
        {
            dynamic total = default(T);
            foreach (T item in source)
            {
                //byte类型，在执行加法运算时，自动提升为了int类型，再强行转换为byte类型会报错
                total = (T)(total + item);
            }
            return total;
        }

        private static int CountImp<T>(ICollection<T> collection)
        {
            return collection.Count;
        }
        private static int CountImp(IEnumerable collection)
        {
            int count = 0;
            foreach (object item in collection)
            {
                count++;
            }
            return count;
        }
        private static int CountImp(string text)
        {
            return text.Length;
        }
        /// <summary>
        /// 动态类型作用3：使用多重分发
        /// 对于静态类型，C#实现的是单一分发：在执行时，所调用的确切方法只取决于覆盖后的方法目标的实际类型；重载是在编译的时候就确定的
        /// 对于动态类型，使用多重分发，会在运行时 根据实参的类型 找出一个最合适的方法实现
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public static void PrintCount(IEnumerable collection)
        {
            dynamic d = collection;
            int count =  CountImp(d);
            Console.WriteLine(count);
        }


        public static void Demo1()
        {
            IEnumerable<string> list = new List<string> { "a","b","c"};
            string item = "d";

            Console.WriteLine(AddContionally1(list,item));

        }

        public static void Demo2()
        {
            var times = new List<TimeSpan> { new TimeSpan( hours:2,minutes: 0,seconds: 0),new TimeSpan(hours: 1, minutes: 20, seconds: 0) };
            Console.WriteLine(times.DynamicSum().TotalMinutes);
            
            var bytes = new Byte[] { 2,8};
            Console.WriteLine(bytes.DynamicSum());

        }

        public static void Demo3()
        {
            PrintCount("ABC");
            PrintCount(new HashSet<int> { 1,2});
        }
    }
}
