using System;
using System.Reflection;

namespace ConsoleApp1
{
    internal class ArrayFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public ArrayFixture(Type t) : base(t)
        {

        }

        public new object[] Fill(int size)
        {
            if (TargetType.IsPrimitive)
                return new PrimitiveFixture(t).Fill(size);

            else if (TargetType == typeof(string))
            {
                return new StringFixture(t).Fill(size);

            }

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (TargetType.IsValueType ||
                TargetType.IsClass || TargetType.IsInterface)
                return new ComplexFixture(t).Fill(size);

            else return null;
        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            throw new System.NotImplementedException();
        }

        public new void SetValue(PropertyInfo p, Object target)
        {
            object [] o = Fill(rnd.Next());
            
            p.SetValue(target, o);

        }

        public new void SetValue(FieldInfo f, Object target)
        {
            object[] o = Fill(rnd.Next());

            f.SetValue(target, o);

        }
    }
}