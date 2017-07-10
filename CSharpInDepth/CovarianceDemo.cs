using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    /// <summary>
    /// C#中 方法的返回值是抗变的（逆变的）。
    ///      方法的参数值是协变的。
    /// Covariance:能够使用比原始指定的类型派生程度更大的类型
    /// </summary>
    public class CovarianceDemo
    {
        public void Demo()
        {
            IIndex<Rectangle> rectangles = RectangleCollection.GetRectangles();
            IIndex<Sharp> sharps = rectangles;

            for (int i=0;i< sharps.Count;i++)
                Console.WriteLine(sharps[i]);
        }
        /// <summary>
        /// IEnumerable<out T>协变的接口
        /// </summary>
        public void Test()
        {
            IEnumerable<Rectangle> rectangle = new List<Rectangle>();
            IEnumerable<Sharp> sharps = rectangle;
        }
    }
    /// <summary>
    /// 泛型接口中添加OUT关键字，表示泛型接口与类型T是协变的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIndex<out T>
    {
        T this[int index] { get; }
        int Count { get; }
    }


    public class RectangleCollection : IIndex<Rectangle>
    {
        public Rectangle[] datas = new Rectangle[] {
            new Rectangle { Width = 10, Heigh= 1 },
            new Rectangle { Width = 12, Heigh= 1},
            new Rectangle { Width = 13, Heigh= 1}
        };
        private static RectangleCollection cols;
        public static RectangleCollection GetRectangles()
        {
            return cols ?? (cols=new RectangleCollection());
        }
        public Rectangle this[int index]
        {
            get
            {
                if (index < 0 || index >= datas.Length)
                    throw new ArgumentOutOfRangeException("index");
                return datas[index];
            }
        }

        public int Count
        {
            get
            {
                return datas.Length;
            }
        }
    }


    public class Sharp : IComparable<Sharp>
    {
        public double Width { get; set; }
        public double Heigh { get; set; }

        public override string ToString()
        {
            return String.Format("Width:{0},Heigh:{1}", Width, Heigh);
        }

        public int CompareTo(Sharp other)
        {
            return this.Width.CompareTo(other.Width);
        }
    }

    public class Rectangle : Sharp
    {
    }
}
