using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    /// <summary>
    /// DynamicObject类型 必须通过继承来实现
    /// DynamicObject对比expandoObject 增加了动态运行时的 自定义实现的各类操作
    /// 这样我们可以更多的去实现自己一些动态的组件
    /// 参考来源：http://www.cnblogs.com/Henllyee/archive/2010/07/05/DynamicObject1.html
    /// </summary>
    public class DynamicObjectDemo:DynamicObject
    {
         Dictionary<string, object> _dict = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dict[binder.Name] = value;
            return true; ;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            var name = binder.Name;
            return _dict.TryGetValue(name,out result);
        }
        /// <summary>
        /// 根据自己需要实现转换函数：按照不同的类型转换不同的结果
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.Equals(typeof(String)))
            {
                if (_dict["FirstName"] != null && _dict["LastName"] != null)
                {
                    result = _dict["FirstName"].ToString() + _dict["LastName"].ToString();
                }
                else
                {
                    result = string.Empty;
                }
                return true;
            }
            if (binder.Type == typeof(int))
            {
                if (_dict["Age"] != null)
                    result = Convert.ToInt32(_dict["Age"]);
                else
                    result = 0;
                return true;
            }
            return base.TryConvert(binder, out result);
        }
        /// <summary>
        /// 根据自己需要实现二元操作：将加法运算改造成合并对象运算
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="arg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            if(binder.Operation == ExpressionType.Add)
            {
                var sub = new List<dynamic>();
                sub.Add(this);
                sub.Add((dynamic)arg);
                result = sub;

                return true;
            }

            return base.TryBinaryOperation(binder, arg, out result);
        }
    }
    public class DynamicObjectDemoTest
    {
        public void Demo1()
        {
            dynamic employee = new DynamicObjectDemo();
            employee.FirstName = "Henry";
            employee.LastName = "Cui";
            employee.Age = 23;

            Console.WriteLine("Employee's info:Name:{0},Age:{1} ",
                employee.FirstName + employee.LastName,
                employee.Age);
        }
        /// <summary>
        /// 测试转换
        /// </summary>
        public void Demo2()
        {
            dynamic employee = new DynamicObjectDemo();
            employee.FirstName = "Henry";
            employee.LastName = "Cui";
            employee.Age = 23;

            var name = (string)employee;
            int age = (int)employee;

            Console.WriteLine(name);
            Console.WriteLine(age);
        }
        /// <summry>
        /// 测试二元运算
        /// </summary>
        public void Demo3()
        {
            dynamic employee = new DynamicObjectDemo();
            employee.FirstName = "Henry";
            employee.LastName = "Cui";
            dynamic employee1 = new DynamicObjectDemo();
            employee1.FirstName = "Hello";
            employee1.LastName = "Ketty";

            var list = new List<dynamic>();
            list = employee + employee1;

            foreach (dynamic em in list)
            {
                Console.WriteLine("Employee's Name was {0}",
                    em.FirstName + em.LastName);
            }
        }
    }
}
