using System;
using System.Reflection;

namespace ConsoleApp1
{
    internal class ArrayFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public ArrayFixture(PropertyInfo p) : base(p)
        {
       
        }

        public ArrayFixture(FieldInfo f) : base(f)
        {
        }

        public new object[] Fill(int size)
        {
            if (TargetType.IsPrimitive)
                return new PrimitiveFixture(p).Fill(size);

            else if (TargetType == typeof(string))
            {
                return new StringFixture(p).Fill(size);

            }

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (TargetType.IsValueType ||
                TargetType.IsClass || TargetType.IsInterface)
                return new ComplexFixture(p).Fill(size);

            else return null;
        }

        public override object New()
        {
            throw new System.NotImplementedException();
        }

        public new void SetValue(Object target)
        {
            object [] o = Fill(rnd.Next());
            if (p != null)
            {
                p.SetValue(target, o);
            }
            if (f != null)
            {
                f.SetValue(target, o);
            }

        }
    }
}