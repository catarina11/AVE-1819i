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
        Random rnd = new Random();
        protected readonly Type t;
        public GeneratorIFixture(Type t)
        {
            this.t = t;
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

        public void SetValue(PropertyInfo p, Object target, object[] obj)
        {
           Object o;
           if (obj.GetType() == typeof(Object[]))
               o = generateValueRandomOfArray(obj);
            else
                o = New();
            p.SetValue(target, o);  
        }

        public void SetValue(FieldInfo f, Object target, object[] obj)
        {
            object o = New();
            f.SetValue(target, o);
        }

        public abstract IFixture Member(string v);

        public IFixture Member(string name, params object[] pool)
        {
            throw new NotImplementedException();
        }

        public IFixture Member(string name, IFixture fixt)
        {
            throw new NotImplementedException();
        }

        private object generateValueRandomOfArray(object[] pool)
        {
            //gerar valor random do array pool
            Object obj = new object();
            obj = pool[rnd.Next(0, pool.Length)];
            return obj;
        }
    }
}
