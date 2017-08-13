using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_Chapter
{
    public class LinqDemo
    {
        public void Demo1()
        {
            var result = from left in Enumerable.Range(1, 4)
                         from right in Enumerable.Range(11, left)
                         select new { x = left, y = right };
            foreach (var item in result)
                Console.WriteLine("x: "+item.x+"y: "+item.y);
        }
        public void SelectManyDemo()
        {
            //处理是流式的，左边序列的每一个元素，用来生成右边的序列
            //第一个参数表示 一对多函数（由左侧的一个元素，得到另一个集合序列）
            //第二个参数表示 对左侧元素和新的集合序列进行投影
            var result = Enumerable.Range(1, 4)
                .SelectMany(left=>Enumerable.Range(11,left), (left, right) => new { x = left, y = right });
        }
    }
}
