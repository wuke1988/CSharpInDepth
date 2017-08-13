using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Chapter
{
    public class PythonScopeDemo
    {
        public void Demo1()
        {
            string python = @"
def sayHello(user):
    print 'Hello %(name)s' %{'name':user}
";
            //代表一种脚本语言引擎
            ScriptEngine engine = Python.CreateEngine();
            //作用域，代表语言代码的执行单元，它是一个全局的范围，所有的代码都可以放进去执行
            ScriptScope scope = engine.CreateScope();
            //引擎在作用域中执行语句
            engine.Execute(python,scope);

            dynamic func = scope.GetVariable("sayHello");
            func("John");
        }
    }
}
