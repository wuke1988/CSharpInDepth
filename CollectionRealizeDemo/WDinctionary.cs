using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRealizeDemo
{
    /// <summary>
    /// 通过散列函数实现散列存储，实际上就是将顺序存储 改为了可通过散列函数快速操作
    /// 散列函数：  int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
    /// 散列表结点： Entry，散列表 entities  Entry[];
    /// 哈希值的数组 bucket int[];
    /// Dictionary扩容的时候会先把当前的容量*2，然后再在一个素数表中找到比这个值大的最近的一个素数。
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class WDinctionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback
    {
        //散列表结点
        private struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }


        //散列值的数组
        private int[] bucket;
        //存储结点的数组
        private Entry[] entities;
        private IEqualityComparer<TKey> comparer;
        private int freeList;
        private int freeCount;

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        ICollection IDictionary.Keys => throw new NotImplementedException();

        ICollection IDictionary.Values => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            if (bucket != null)
            {
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                int targetBucket = hashCode % bucket.Length;
                int last = -1;
                for (int i = bucket[targetBucket]; i >= 0; last = i,i = entities[i].next)
                {
                    if (entities[i].hashCode == hashCode && comparer.Equals(entities[i].key, key))
                    {
                        if (last < 0)
                        {
                            bucket[targetBucket] = entities[i].next;
                        }
                        else
                        {
                            entities[last].next = entities[i].next;
                        }
                        entities[i].hashCode = -1;
                        entities[i].next = freeList;
                        entities[i].key = default(TKey);
                        entities[i].value = default(TValue);
                        freeList = i;
                        freeCount++;

                        return true;
                    }
                }                
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Insert(item.Key, item.Value, true);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = FindEntry(item.Key);

            if (index > 0 && EqualityComparer<TValue>.Default.Equals(item.Value, entities[index].value))
            {
                Remove(item.Key);
                return true;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private int FindEntry(TKey key)
        {
            int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % bucket.Length;

            for (int i = bucket[targetBucket]; i >= 0; i = entities[i].next)
            {
                if (entities[i].hashCode == hashCode && comparer.Equals(entities[i].key, key))
                {
                    return i;
                }              
            }
            return -1;
        }

        private void Insert(TKey key,TValue value,bool add)
        {
            int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;

            int targetBucket = hashCode % bucket.Length;

            for (int i = bucket[targetBucket]; i >= 0; i = entities[i].next)
            {
                if (entities[i].hashCode==hashCode&&comparer.Equals(entities[i].key,key))
                {
                    entities[i].value = value;
                }
            }

            int index = freeList; ;
            if (freeCount > 0)
            {
                
                freeList = entities[index].next;
                freeCount--;
            }

            entities[index].key = key;
            entities[index].value = value;
            entities[index].next = bucket[targetBucket];
            entities[index].hashCode = hashCode;
            
        }

        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }

        public void Add(object key, object value)
        {
            throw new NotImplementedException();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
