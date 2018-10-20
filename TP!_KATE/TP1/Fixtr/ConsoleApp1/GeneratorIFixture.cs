using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

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

        public void SetValue(PropertyInfo p, Object target, object[] obj, IFixture fixt)
        {
           Object o;
           if (obj != null && obj.GetType() == typeof(Object[]))
            {
                Object randomValueofArray = generateValueRandomOfArray(obj);
                if (randomValueofArray.GetType().Equals(typeof(int)))
                {
                    if (Validation.NumberHas5Digits((int)randomValueofArray))
                        o = randomValueofArray;
                    else
                        o = null;
                }
                else
                    o = randomValueofArray;
            }
               
           else if (fixt != null)
            {
                DateTime dt=new DateTime();
                Object objAux = new FixtureDateTime().New();
                dt = (DateTime)objAux;
                String st = objAux.ToString();

                //DateTime.TryParse(objAux.ToString(), out dt); //DateTime
                if (dt.GetType().Equals(typeof(DateTime)))
                {
                    if (Validation.DateIsGreaterThan1980(dt))
                        o = objAux;
                    else
                        o = null;
                }
                else
                    o = objAux;
            }    
           else
               o = New();

            p.SetValue(target, o);  
        }

        public void SetValue(FieldInfo f, Object target, object[] obj, IFixture fixt)
        {
            object o;
            if (obj!= null && obj.GetType() == typeof(Object[]))
            {
                Object randomValueofArray = generateValueRandomOfArray(obj);
                if (randomValueofArray.GetType().Equals(typeof(int)))
                {
                    if (Validation.NumberHas5Digits((int)randomValueofArray))
                        o = randomValueofArray;
                    else
                        o = null;
                }
                else
                    o = randomValueofArray;
            }
            else if (fixt != null)
            {
                DateTime dt = new DateTime();
                Object objAux = new FixtureDateTime().New();
                dt = (DateTime) objAux;
                String st = objAux.ToString();

                if (dt.GetType().Equals(typeof(DateTime)))
                {
                    if (Validation.DateIsGreaterThan1980(dt))
                        o = objAux;
                    else o = null;
                }
                else
                    o = objAux;
            }
            else
                o = New();
           
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
