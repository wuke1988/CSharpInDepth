using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    /// <summary>
    /// 实践ExpandoObject类型，实现了以下接口
    /// IDynamicMetaObjectProvider
    /// IDictionary<string, object>, =>可以像字典一样使用 通过名称存储对象
    /// ICollection<KeyValuePair<string, object>> 
    /// IEnumerable<KeyValuePair<string, object>>
    /// IEnumerable
    /// INotifyPropertyChanged
    /// 参考：http://www.cnblogs.com/Henllyee/archive/2010/06/20/ExpandoObject1.html
    /// </summary>
    public class ExpandoObjectDemo
    {

        /// <summary>
        /// 这个类时在运行时解析的,这样我们就会带来性能上的一些损失
        /// 而且,程序的逻辑复杂性越高,越难发现问题所在
        /// 对于简单的对象,我们可以使用这个类,复杂的就不要使用了
        /// </summary>
        public void Demo1()
        {
            dynamic expando = new ExpandoObject();
           
            //添加（属性变化）事件
            ((INotifyPropertyChanged)expando).PropertyChanged += ExpandoObjectDemo_PropertyChanged;

            //添加属性
            expando.First = "Hello World";
            //添加方法
            expando.Method = (Func<int, int>)(x => x + 1);

            //添加事件
            expando.SayHello = null;
            expando.SayHello += new EventHandler(SayHello);

            Console.WriteLine(expando.First);
            
            Console.WriteLine(expando.Method(2));

            expando.SayHello(expando,null);

            //移除成员
            ((IDictionary<string, object>)expando).Remove("SayHello");

        }

        private void SayHello(object sender,EventArgs e)
        {
            dynamic obj = sender as dynamic;
            Console.WriteLine(obj.First+" From Event!");
        }

        private void ExpandoObjectDemo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("{0}  {1} Changed!",sender.ToString(),e.PropertyName);
        }
    }
}
