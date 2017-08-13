using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _9_Chapter
{
    public class BuildExpressionTree
    {
        /// <summary>
        /// 用lambda创建表达式树
        /// </summary>
        public void Demo1()
        {
            Expression<Func<string, string, bool>> expression =
                (x, y) => x.StartsWith(y);

            var compiled = expression.Compile();

            Console.WriteLine(compiled("First","Second"));
            Console.WriteLine(compiled("First","Fir"));
        }
        /// <summary>
        /// 针对 linq to Sql 等情况 ，无法将lambda编译为表达式树，需要手动创建表达式树
        /// 创建：x.StartWith(y)对应的表达式树
        /// </summary>
        public void Demo2()
        {
            //1.首先声明方法本身
            MethodInfo method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            //2.创建涉及的参数表达式
            ParameterExpression target = Expression.Parameter(typeof(string), "x");
            ParameterExpression methodArg = Expression.Parameter(typeof(string), "y");

            Expression[] methodArgs = new Expression[] { methodArg };
            //3.创建方法表达式
            Expression call = Expression.Call(target, method, methodArgs);


            var lambdaParameters = new[] { target, methodArg };

            //4.将方法表达式转换成lambda表达式
            Expression<Func<string, string, bool>> expression
                = Expression.Lambda<Func<string, string, bool>>(call, lambdaParameters);

            var compiled = expression.Compile();

            Console.WriteLine(compiled("Hello,World", "Hello"));
        }

        public void Demo3()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(int),"a");
            ParameterExpression parameter2 = Expression.Parameter(typeof(int),"b");
            //创建方法表达式
            BinaryExpression binary = BinaryExpression.Add(parameter1,parameter2);

            //将方法表达式转换为Lambda表达式
            Expression<Func<int, int, int>> expression = 
                Expression.Lambda<Func<int, int, int>>(binary,new[] { parameter1,parameter2});

            var compiled = expression.Compile();

            Console.WriteLine(compiled(4,5));
        }
    }
}
