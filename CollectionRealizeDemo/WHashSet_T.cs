using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
   public class WHashSet_T<T>:ICollection<T>,ISet<T>
    {
        private const int Lower31BitMask = 0x7FFFFFFF;
        private const int StackAllocThreshold = 100;
        private const int ShrinkThreshold = 3;


        private int[] buckets;

        private Slot[] slots;

        private int freeList;

        private int lastIndex;

        private int count;

        private IEqualityComparer<T> comparer;

        internal struct Slot
        {
            internal int HashCode;
            internal T   Value;
            internal int Next;
        }


        #region 构造函数
        
        public WHashSet_T()
        {

        }
        public WHashSet_T(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                this.comparer = EqualityComparer<T>.Default;
            }
            else
            {
                this.comparer = comparer;
            }
            freeList = -1;
            lastIndex = 0;
            count = 0;
        }

        public WHashSet_T(IEnumerable<T> collection)
            :this(EqualityComparer<T>.Default)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            ICollection<T> coll = collection as ICollection<T>;
            if (coll != null)
            {
                //初始化 哈希数组 散列结点数组
                Initialize(coll.Count);
                //将数组合并到集合中
                this.UnionWith(collection);
                //如果散列结点数组的大小超出散列结点数组个数的 ShrinkThreshold倍
                if (count > 0 && slots.Length / count > ShrinkThreshold)
                {
                    TrimExcess();
                }
            }
        }



        #endregion
        /// <summary>
        /// 作用：在用Collection创建Set时，压缩Set的空间大小
        /// 压缩散列结点数组，将数组的大小压缩到数组的元素个数
        /// 原理：按照 散列结点数组元素的个数多少来重新创建 哈希数组 和 散列结点数组
        /// 循环原所有的散列结点数组，将其中不为空的 复制到新数组中
        /// 最终用新数组代替旧数组
        /// </summary>
        private void TrimExcess()
        {
            Slot[] newSlots = new Slot[count];
            int[] newBuckets = new int[count];

            int newIndex = 0;
            for (int i=0;i<lastIndex;i++)
            {
                if (slots[i].HashCode > 0)
                {
                    newSlots[newIndex] = slots[i];

                    int bucket = newSlots[newIndex].HashCode % count;
                    newSlots[newIndex].Next = newBuckets[bucket] - 1;
                    newBuckets[bucket] = newIndex + 1;

                    newIndex++;
                }
            }
            lastIndex = newIndex;
            slots = newSlots;
            buckets = newBuckets;
            freeList = -1;
        }
        private void Initialize(int capacity)
        {
            buckets = new int[capacity];
            slots = new Slot[capacity];
        }

        /// <summary>
        /// 如果元素不存在，则将value添加到集合中，返回true；
        /// 否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool AddIfNotPresent(T value)
        {
            int hashCode = value.GetHashCode() % Lower31BitMask;
            int bucket = hashCode % slots.Length;
            int collisionCount = 0;


            for (int i = buckets[bucket] - 1; i >= 0;  i = slots[i].Next)
            {
                if (hashCode == slots[i].HashCode && comparer.Equals(value, slots[i].Value))
                {
                    return false;
                }
                collisionCount++;
            }

            int index;

            //如果内存空间有空，则往后挪一个空间
            if (freeList >= 0)
            {
                index = freeList;
                freeList = slots[index].Next;
            }
            else
            {
                //如果空间不够，自动分配新的空间
                if (lastIndex == slots.Length)
                {
                    IncreaseCapacity();
                    bucket = hashCode % buckets.Length;
                }
                index = lastIndex;
                lastIndex++;
            }

            //给当前节点增加新值
            slots[index].HashCode = hashCode;
            slots[index].Value = value;
            slots[index].Next = buckets[bucket] - 1;
            buckets[bucket] = index + 1;
            count++;

            return true;
        }
        private void IncreaseCapacity()
        {

        }


        #region 实现ICollection接口
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {

            AddIfNotPresent(item);
        }

        /// <summary>
        /// 仍然采用Array的方法
        /// </summary>
        public void Clear()
        {
            Array.Clear(slots, 0, lastIndex);
            Array.Clear(buckets, 0, buckets.Length);
            lastIndex = 0;
            count = 0;
            freeList = -1;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

       

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 实现ISet接口

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            int hashCode = item.GetHashCode() % Lower31BitMask;
            int bucket = hashCode % slots.Length;
            int last = -1;


            for(int i = buckets[bucket]-1; i>=0;last =i,i=slots[i].Next)
            {
                if (hashCode == slots[i].HashCode && comparer.Equals(item, slots[i].Value))
                {
                    if(last<0)
                        buckets[bucket] = slots[i].Next + 1;
                    else
                        slots[last].Next = slots[i].Next;
                    slots[i].Value = default(T);
                    slots[i].HashCode = -1;
                    slots[i].Next = freeList;

                    count--;
                }


                if (count == 0)
                {
                    lastIndex = 0;
                    freeList = -1;
                }
                else
                    freeList = i;

                return true;

            }
            return false;

        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        bool ISet<T>.Add(T item)
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
