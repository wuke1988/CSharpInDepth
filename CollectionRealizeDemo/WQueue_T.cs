using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// 队列：实现先进先出的集合
    /// 仍是基于数组来实现，用两个索引分别表示 头指针和尾指针
    /// 
    /// </summary>
    class WQueue_T<T> : IEnumerable<T>, ICollection
    {
        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;


        static T[] _emptyArray = new T[0];
        private const int _DefaultCapacity = 4;
        private const int _GrowFactor = 200;
        private const int _MininumGrow = 4;
        private const int _ShrinkThreshold = 32;

        #region 构造函数

        public WQueue_T()
        {
            _array = _emptyArray;
        }
        public WQueue_T(IEnumerable<T> collection)
        {
            if (collection != null)
                throw new ArgumentNullException("collection");
            _array = new T[_DefaultCapacity];
            _size = 0;
            _head = 0;
            _tail = 0;

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    EnQueue(enumerator.Current);
                }
            }

        }

        public WQueue_T(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("capacity");

            _array = new T[capacity];
            _size = 0;
            _head = 0;
            _tail = 0;

        }
        #endregion

        public int Count => _size;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        /// <summary>
        /// 设置内存容量大小
        /// 根据头结点和尾结点的不同位置来进行复制
        /// </summary>
        /// <param name="capacity"></param>
        private void SetCapacity(int capacity)
        {
            T[] newarray = new T[capacity];
            if (_head < _tail)
            {
                Array.Copy(_array, _head, newarray, 0, _size);
            }
            else
            {
                Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
                Array.Copy(_array,0,newarray,_array.Length-_head,_tail);
            }
            _array = newarray;
            _head = 0;
            _tail = (_size == capacity) ? 0 : _size;
        }
        internal  T GetElement(int i)
        {
            return _array[(_head + i) % _array.Length];
        }
        /// <summary>
        /// 入队，内存扩展策略,每次扩展2倍（至少为当前容量+最小增长量）
        /// </summary>
        /// <param name="value"></param>
        public void EnQueue(T value)
        {
            if (_size == _array.Length)
            {
                int newCapacity = _array.Length * _GrowFactor / 100;

                if (newCapacity < _array.Length + _MininumGrow)
                {
                    newCapacity = _array.Length + _MininumGrow;
                }

                SetCapacity(newCapacity);
            }

            _array[_tail] = value;
            _size++;
            _tail = (_tail+1) % _array.Length;
        }
        public T DeQueue()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            T removed = _array[_head];
            _array[_head] = default(T);

            _head = (_head + 1) % _array.Length;
            _size--;
            return removed;

        }
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        struct Enumerator : IEnumerator<T>
        {
            public T Current
            {
                get
                {
                    return _currentElement;
                }
            }

            private int _index;
            private WQueue_T<T> queue;
            private T _currentElement;

            public Enumerator(WQueue_T<T> q)
            {
                _index = -1;
                queue = q;
                _currentElement = default(T);
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _index = -2;
                _currentElement = default(T);
            }

            public bool MoveNext()
            {
                if (_index == -2)
                    return false;
                _index++;

                if (_index == queue.Count)
                {
                    _index = -2;
                    _currentElement = default(T);
                    return false;
                }

                _currentElement = queue.GetElement(_index);

                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
