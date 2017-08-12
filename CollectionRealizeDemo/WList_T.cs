using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// List<T>实现IList<T>,IList,IReadonly接口
    /// 实现包括了内存分配，ICollction<T>接口，IList<T>接口
    /// 函数实现基于Array的方法：诸如 Array.Copy  Array.IndexOf  Array.Clear
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WList_T<T> : IList<T>, IList, IReadOnlyList<T>
    {
        T[] _items;

        private readonly T[] _emptyArray = new T[0];

        //数组元素的大小
        private int _size;
        private int  _defaultCapacity = 4;
        private int _maxArrayLength = 1000;

        #region 实现构造函数
        public WList_T()
        {
            _items = _emptyArray;
        }
        public WList_T(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            ICollection<T> c = collection as ICollection<T>;
            if (c != null)
            {
                int count = c.Count;
                if (count == 0)
                {
                    _items = _emptyArray;
                }
                else
                {
                    _items = new T[count];
                    c.CopyTo(_items, 0);
                    _size = count;
                }
            }
            else
            {
                //foreach (T item in collection)
                //    Add(item);
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Add(enumerator.Current);
                        _size++;
                    }
                }
                  
            }
        }
        public WList_T(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new T[capacity];
        }
         #endregion

        
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public bool IsReadOnly => false;

        public bool IsFixedSize =>false;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized =>false;

        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                { 
                    if (value != _items.Length)
                    {
                        if (value > 0)
                        {
                            T[] newItems = new T[Capacity];

                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        else
                        {
                            _items = _emptyArray;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 分配策略：如果当前数组容量为0，则采用默认容量；否则为当期数组容量*2
        /// 如果新数组容量为大于最大容量，则用最大容量
        /// 如果新数组容量小于最小容量，则用最小容量
        /// </summary>
        /// <param name="min"></param>
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity :_items.Length*2;
                if (newCapacity > _maxArrayLength)
                    newCapacity = _maxArrayLength;
                if (newCapacity < min)
                    newCapacity = min;
                Capacity = newCapacity;
            }
        }


        #region IList需要实现的接口 （实现定位）

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > _size)
                    throw new ArgumentOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index > _size)
                    throw new ArgumentOutOfRangeException();
                _items[index] = value;
            }
        }
        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }
        public int IndexOf(T item)
        {
           return Array.IndexOf(_items,item);
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public void Insert(int index, T item)
        {
            EnsureCapacity(_size+1);
            Array.Copy(_items, index, _items, index + 1, _size - index);
            _items[index] = item;
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection要求实现的方法（实现可变）
        public void Add(T item)
        {
            if (_size == _items.Length)
                EnsureCapacity(_size+1);
            _items[_size++] = item;
        }

        public int Add(object value)
        {
            Add((T)value);
            return Count - 1;
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _items.Length);
                _size = 0;
            }

        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                foreach (T t in _items)
                {
                    if (t == null)
                        return true;
                }
                return false;
            }
            else
            {
                EqualityComparer<T> c = EqualityComparer<T>.Default;
                for(int i=0;i<_size;i++)
                {
                    if (c.Equals(item, _items[i]))
                        return true;
                }
                return false;
            }

        }

        public bool Contains(object value)
        {
            T item = (T)value;
            return Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_items,0,array,arrayIndex,_size);
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(_items, 0, array, index, _size);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index > 0 && index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
                _items[_size] = default(T);
                return true;
            }
            else
                return false;
        }
        public void Remove(object value)
        {
             Remove((T)value);
        }

        #endregion

        #region IEnumerable<T>要实现的接口
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
