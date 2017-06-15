using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    class CountingEnumerable : IEnumerable<int>
    {        
        public IEnumerator<int> GetEnumerator()
        {
            return new CountingEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
    class CountingEnumerator : IEnumerator<int>
    {
        private int current = 0;
        public int Current
        {
            get
            {
               return current;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }
        public void Dispose()
        {
            
        }
        public bool MoveNext()
        {
            current++;
            return current < 10;
        }
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
