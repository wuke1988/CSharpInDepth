using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
   static  class Reflect
    {
       public static void Test()
        {
            string listTypeName = "System.Collections.Generic.List^1";

            Type type = Type.GetType(listTypeName);

            Type closedByName = Type.GetType(listTypeName+"[System.String]");

           

            Type closedByTypeof = typeof(List<>);


            //Type closedByMethod = type.MakeGenericType(typeof(string));

            Type closedByMethod = closedByTypeof.MakeGenericType(typeof(string));

            closedByTypeof.GetMethods();

            closedByMethod.GetMethods();
           
        }
    }
}
