using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
   static  class Reflect
    {
        static void Test()
        {
            string listTypeName = "System.Collections.Generic.List^1";

            Type type = Type.GetType(listTypeName);

            Type closedByMethod = type.MakeGenericType(typeof(string));

            closedByMethod.GetMethods();
           
        }
    }
}
