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
    /// DynamicObject对象来实现XElement
    /// 参考地址：http://www.cnblogs.com/Henllyee/archive/2010/07/11/1775302.html
    /// </summary>
    public class DynamicXNode :DynamicObject
    {
        private XElement _xElement;

        public DynamicXNode(string name)
        {
            _xElement = new XElement(name);
        }
        public DynamicXNode(XElement node)
        {
            _xElement = node;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            return base.TryGetMember(binder, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var node = _xElement.Element(binder.Name);
            if (node != null)
            {
                node.SetValue(value);
            }
            else
            {
                if (value is DynamicObject)
                {
                    _xElement.Add(new XElement(binder.Name));
                }
                else
                    _xElement.Add(new XElement(binder.Name,value.ToString()));

            }
            return true;
        }
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.Equals(typeof(XElement)))
            {
                result = _xElement;
                return true;
            }
            return base.TryConvert(binder, out result);
        }
    }
    public class DynamicObjectDemo2
    {
        public void Demo1()
        {
            dynamic employee = new DynamicXNode("Employee");
            employee.Name = new DynamicXNode("Name");
            employee.Birthday = "1987-10-14";

            employee.Name.FirstName = "Henry";
            employee.Name.LastName = "Cui";

            

            Console.WriteLine(employee.Name.FirstName);
        }
    }
}
