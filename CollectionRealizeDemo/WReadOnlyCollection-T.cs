using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// ReadOnly是通过 将IsReadOnly属性置为真
    /// 限制 改变集合的方法的访问性
    /// 或 对更改集合的部分方法抛出不支持的异常 来实现 只读特性的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class WReadOnlyCollection_T<T> : IList<T>, IList, IReadOnlyList<T>
    {
        IList<T> list;

        public WReadOnlyCollection_T(IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list)); 
            this.list = list;
        }

        public T this[int index]
        {
            get { return list[index]; }
            set { throw new NotSupportedException(); }
        }

        object IList.this[int index]
        {
            get { return list[index]; }
            set => throw new  NotSupportedException();
        }     

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly { get { return true; } }

        public bool IsFixedSize => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public int Add(object value)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            if (list.Contains(item))
                return true;
            else
                return false;
        }

        public bool Contains(object value)
        {
            if (list.Contains((T)value))
                return true;
            else
                return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Length - arrayIndex < Count)
            {
                throw new Exception();
            }
            else
            {
                int index = arrayIndex;

                for(int i=0;i<Count;i++)
                    array[index++] = list[i];
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public int IndexOf(object value)
        {
            return list.IndexOf((T)value);
        }

        #region 保护方法-限制改变集合元素的相关方法的访问性
        void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

         void Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

         bool Remove(T item)
        {
            throw new NotSupportedException();
        }

         void Remove(object value)
        {
            throw new NotSupportedException();
        }

         void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
       #endregion
    }
}
