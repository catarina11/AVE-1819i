using System;
using System.Reflection;

namespace ConsoleApp1
{
    internal class DefaultFixture : GeneratorIFixture
    {
        public DefaultFixture(FieldInfo f) : base(f)
        {
        }

        public DefaultFixture(PropertyInfo p) : base(p)
        {
        }

        public override object New()
        {
            Type t;
            Object ret=null;
            if (p!=null)
            {
                t= p.PropertyType;
                ret = t.IsValueType ? Activator.CreateInstance(t) : null;
            }
            if (f != null)
            {
                t = f.FieldType;
                ret = t.IsValueType ? Activator.CreateInstance(t) : null;
            }
            return ret;
            
        }
    }
}