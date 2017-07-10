using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    /// <summary>
    /// 泛型反射相关
    /// </summary>
   static  class Reflect
    {
       public static void Test()
        {
            string listTypeName = "System.Collections.Generic.List^1";

            Type type = Type.GetType(listTypeName);

            //通过名称获取已构造泛型类型
            Type closedByName = Type.GetType(listTypeName+"[System.String]");

           
            //通过typeof获取未绑定泛型类型
            Type closedByTypeof = typeof(List<>);


            //Type closedByMethod = type.MakeGenericType(typeof(string));

            //通过MakeGenericType方法对未绑定泛型进行构造
            Type closedByMethod = closedByTypeof.MakeGenericType(typeof(string));

            closedByTypeof.GetMethods();

            closedByMethod.GetMethods();
           
        }
    }
}
