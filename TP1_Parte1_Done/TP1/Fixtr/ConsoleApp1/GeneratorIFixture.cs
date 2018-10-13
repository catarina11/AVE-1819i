using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{


    public abstract class GeneratorIFixture : IFixture
    {

        protected PropertyInfo p;
        protected FieldInfo f;
        protected readonly Type t;
        public GeneratorIFixture(PropertyInfo p)
        {
            this.p = p;
            t = p.PropertyType;
            f = null;
        }
        public GeneratorIFixture(FieldInfo f)
        {
            this.f = f;
            t = f.FieldType;
            this.p = null;
        }

        public Type TargetType { get => t; }

        public object[] Fill(int size)
        {
            object[] res = new object[size];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = New();
            }
            return res;
        }
        public abstract object New();

        public void SetValue(Object target)
        {
            object o = New();
            if (p != null)
            {
                p.SetValue(target, o);
            }
            if(f != null)
            {
                f.SetValue(target, o);
            }

        }
    }
}
