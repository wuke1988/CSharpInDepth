using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// 什么时候列表不是List,当是LinkList时
    /// LinkList是保持了元素的添加顺序的集合。但LinkList不支持索引的方式访问，所以不是List
    /// LinkList是双向链表：包含头结点和尾结点，每个结点包含前后两个结点的引用;包含Count;
    /// LinkList在空间维护上要比List要低，但是对于任意元素位置的插入操作效率高 O(1)
    /// </summary>
    public class LinkedListDemo
    {
        public void Demo()
        {
            string[] strs = new string[] { "One","Two","Three","Four","Five","Six"};

            LinkedList<string> linkedList = new LinkedList<string>(strs);

            linkedList.AddLast(new LinkedListNode<string>("Seven"));

            Display(linkedList);

            
        }

        public void Display(LinkedList<string> list)
        {
            LinkedListNode<string> node = list.First;

            if (node != null)
            {
                do
                {                  
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
                while (node!=null);
            }
        }
        public void Display2(LinkedList<string> list)
        {
            foreach (string node in list)
            {
                Console.WriteLine(node);
            }
        }
    }
}
