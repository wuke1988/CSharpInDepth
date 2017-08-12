using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    public class QueueDemo
    {
        public void Demo()
        {
            //创建一个队列
            Queue<string> myQ = new Queue<string>();
            myQ.Enqueue("The");//入队
            myQ.Enqueue("quick");
            myQ.Enqueue("brown");
            myQ.Enqueue("fox");
            myQ.Enqueue(null);//添加null
            myQ.Enqueue("fox");//添加重复的元素


            // 打印队列的数量和值
            Console.WriteLine("myQ");
            Console.WriteLine("\tCount:    {0}", myQ.Count);

            // 打印队列中的第一个元素，并移除
            Console.WriteLine("(Dequeue)\t{0}", myQ.Dequeue());
        }
    }
}
