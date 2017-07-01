using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _12Linq
{
    public class ProductQuery<T> : IQueryable<T>
    {
        public Type ElementType
        {
            get;
            private set;
        }

        public Expression Expression
        {
            get;
            private set;
        }

        public IQueryProvider Provider
        {
            get;
            private set;
        }

        internal ProductQuery(IQueryProvider provider, Expression expression)
        {
            Expression = expression;
            Provider = provider;
        }
        internal ProductQuery()
            : this(new ProductQueryProvider(), null)
        {
            Expression = Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class ProductQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Console.WriteLine("IQueryable-CreateQuery Invoke " + expression.ToString());
            Type type = typeof(ProductQuery<>).MakeGenericType(expression.Type);
            object[] param = new object[] { this,expression};
            return (IQueryable)Activator.CreateInstance(type, param);
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            Console.WriteLine("IQueryable<T>-CreateQuery Invoke " + expression.ToString());
            return new ProductQuery<T>(this,expression);
        }

        public object Execute(Expression expression)
        {
            Console.WriteLine("object-Execute Invoke " + expression.ToString());
            return null;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            Console.WriteLine("TResult-Execute Invoke " + expression.ToString());
            return default(TResult);
        }
    }

    public class Demo
    {
        public void Test()
        {
            var query = from r in new ProductQuery<String>()
                        where r.StartsWith("ABC")
                        select r.Length;
            double? length = query.Average();

        }
    }
}
