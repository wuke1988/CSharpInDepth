using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Chapter
{
    /// <summary>
    /// 1. 构造函数应为私有的
    /// 2. 类应该是密封的，不能被继承
    /// 3. 应该包含一个静态变量来保存对象实例的引用
    /// 4. 一个公共静态方法来返回对象实例（如果不存在，则创建一个）
    /// 简单实现
    /// </summary>
    public sealed class SingleTonDemo
    {
        private static SingleTonDemo _instance = null;

        private SingleTonDemo() { }


        public static SingleTonDemo Instance
        {
            get { return _instance ??(_instance=new SingleTonDemo()); }
        }
    }

    /// <summary>
    /// 构造对象时加锁
    /// </summary>
    public sealed class SingleTonDemo1
    {
        private static SingleTonDemo1 _instance = null;

        private static readonly object locker = new object();

        private SingleTonDemo1() { }


        public static SingleTonDemo1 Instance
        {
            get
            {
                lock (locker)
                {
                    return _instance ?? (_instance = new SingleTonDemo1());
                }
            }
        }
    }


    /// <summary>
    /// 减少同步操作的次数
    /// </summary>
    public sealed class SingleTonDemo2
    {
        private static SingleTonDemo2 _instance = null;

        private static readonly object locker = new object();

        private SingleTonDemo2() { }


        public static SingleTonDemo2 Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                            _instance = new SingleTonDemo2();
                        
                    }
                }
                return _instance;
            }
        }
    }
}

