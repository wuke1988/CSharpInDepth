using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6Yeild
{
    class WhereImplement
    {
        public static IEnumerable<T> Where<T>(IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null || predicate == null)
                throw new ArgumentNullException();
            return WhereImp(source, predicate);
        }
        public static IEnumerable<T> WhereImp<T>(IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}
