using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    /// <summary>
    /// Contravariance:使你能够使用比原始指定的类型更泛型（派生程度更小）的类型
    /// </summary>
    public class ContravarianceDemo
    {
        public void Demo()
        {
            //IDisplay<Sharp> sharps = new ShapeDisplay();

            IDisplay<Rectangle> Rectangle = new ShapeDisplay();

            Rectangle.Show(new Rectangle { Width=10,Heigh=1});
        }

        public void Test()
        {
           
        }
    }

   

    /// <summary>
    /// 泛型接口中添加IN关键字，表示泛型接口与类型T是抗变的(逆变)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDisplay<in T>
    {
        void Show(T item);             
    }

    public class ShapeDisplay : IDisplay<Sharp>
    {
        public void Show(Sharp item)
        {
            Console.WriteLine("{0} Width:{1},Heigh:{2}",item.GetType().Name,item.Width,item.Heigh);
        }
    }

}
