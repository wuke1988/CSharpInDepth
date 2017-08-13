using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _14_Chapter
{
    /// <summary>
    /// ExpandoObject对象的实际场景使用：构造xml对象
    /// 参考：http://www.cnblogs.com/Henllyee/archive/2010/06/22/ExpandoObject2.html
    /// </summary>
    public class ExpandoObjectDemo2
    {
        //linq to xml的写法
        private XElement CreateXElement()
        {
            var element = new XElement
           (
                "Employee",
                new XElement("FirstName", "Herry"),
                new XElement("LastName", "Tom"),
                new XElement("Age",29),
                new XElement("Company",
                    new XElement("Name","Sharewin"),
                    new XElement("Address","Xi'An")
                    )
                );
            return element;
        }
        /// <summary>
        /// 通过动态类型（ExpandoObject）实现构造Xml更简单
        /// </summary>
        /// <returns></returns>
        private dynamic CreateByExpandoObject()
        {
            dynamic element = new ExpandoObject();
            element.FirstName = "Herry";
            element.LastName = "Tom";
            element.Age = 29;

            element.Company = new List<dynamic>();

            element.Company.Add(new ExpandoObject());
            element.Company[0].Name = "XXXX";
            element.Company[0].Address = "Suzhou China";

            element.Company.Add(new ExpandoObject());
            element.Company[1].Name = "YYYY";
            element.Company[1].Address = "Suzhou China";

            return element;
        }
        /// <summary>
        /// 动态类型与xlment的转换器
        /// </summary>
        /// <param name="eleName"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private XElement ConvertExpandoObjectToXelement(string eleName, dynamic node)
        {
            var xnode = new XElement(eleName);

            foreach (var pro in (IDictionary<string, object>)node)
            {
                if (pro.Value is ExpandoObject)
                {
                    ConvertExpandoObjectToXelement(pro.Key, pro.Value);
                }
                else
                {
                    if (pro.Value is List<dynamic>)
                    {
                        foreach (var child in (List<dynamic>)pro.Value)
                        {
                            xnode.Add(ConvertExpandoObjectToXelement(pro.Key, child));
                        }
                    }
                    xnode.Add(new XElement(pro.Key, pro.Value));
                }
            }
            return xnode;
        }
        /// <summary>
        /// 测试
        /// </summary>
        public void Demo1()
        {
            var company = from c in (List<dynamic>)CreateByExpandoObject().Company
                          select c;

            company.First().Address = "Shanghai China";

            Console.WriteLine(company.First().Name);
            Console.WriteLine(company.First().Address);

        }
        
    }
}
