using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// 基于已经实现的的Collection集合
    /// 实现 INotifyPropertyChanged 和 INotifyCollectionChanged接口
    /// 重写 集合改变的方法 ，在集合发生改变的额时候 引发事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WObserableCollection_T<T> : Collection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected event PropertyChangedEventHandler PropertyChanged;
        protected event NotifyCollectionChangedEventHandler CollectionChanged;

        public WObserableCollection_T()
            : base()
        {

        }

        public WObserableCollection_T(IList<T> list)
            : base(list != null ? new List<T>(list.Count) :list)
        {
            
        }
        public WObserableCollection_T(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            CopyFrom(collection);
        }
       

        #region 限制方法，重载 Collection 中的相关方法，实现发消息
        protected override void RemoveItem(int index)
        {
            T item = Items[index];
            base.RemoveItem(index);
            OnPropertyChanged(CountString);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove,item,index);
        }
        #endregion

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { CollectionChanged += value; }
            remove { CollectionChanged -= value; }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this,args);
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,args);
            }
        }



        #region 私有
        private const string CountString = "Count";

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (T t in collection)
                {
                    Items.Add(t);
                }
            }
        }
        #endregion
    }

    public class WObserableCollection_Test
    {
        public void Test()
        {
           
        }
    }
}
