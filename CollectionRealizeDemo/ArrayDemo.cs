using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    class ArrayDemo
    {
        public void Demo()
        {
            int[] array1 = new int[] {1,2,3,4 };
            int[,] array2 = new int[3, 2] { { 1,2},{ 2,3},{ 3,4} };
            int[][] array3 = new int[3][];

            array3[0] =new int[]{ 1,2,3,4,5,6};
            array3[1] = new int[] { 2,3,4};
            array3[2] = new int[] { 4,5,6,7,8,9,10,11};

            //普通一位数组用法
            Console.WriteLine("Array1,type:"+array1.GetType());
            foreach(int temp in array1)
                Console.Write(temp+" ");
            Console.WriteLine();

            //多维数组用法
            Console.WriteLine("Array2,type:" + array2.GetType());
            foreach (int temp in array2)
            {
                Console.Write(temp + " ");
            }
            Console.WriteLine();

            //锯齿数据用法--注意锯齿数组中的访问方法
            Console.WriteLine("Array3,type:" + array3.GetType());
            for (int i = 0; i < array3.Length; i++)
            {
                for (int k = 0; k < array3[i].Length; k++)
                    Console.Write(array3[i][k] + " ");
                Console.WriteLine();
            }

            //1基数组
            Array array4 = Array.CreateInstance(typeof(int), new int[] { 2 }, new int[] { 1 });
            
            Console.WriteLine(array4.GetType());

            //0基数组
            //new int[] { 2,2}代表每个维度的大小
            //参数 new int[] { 0,0}，决定每个维度的index起始位置
            Array array5 = Array.CreateInstance(typeof(int),new int[] { 2,2},new int[] { 0,0});

            for (int i = array5.GetLowerBound(0); i <= array5.GetUpperBound(0); i++)
            {
                for (int k = array5.GetLowerBound(1); k <= array5.GetUpperBound(1); k++)
                    array5.SetValue(110,new int[] { i,k});
            }
            
            Console.WriteLine(array5.GetType());
        }
    }
}
