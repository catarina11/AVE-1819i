using System;
using System.Reflection;

namespace ConsoleApp1
{
    public class ArrayFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public ArrayFixture(Type t) : base(t)
        {

        }

        public new object[] Fill(int size)
        {
            Type targetType = TargetType.GetElementType();
            if (targetType.IsPrimitive)
                return Dictionary.GetPrimitiveGenerator(targetType).Fill(size);

            else if (targetType == typeof(string))
                return Dictionary.GetStringGenerator(targetType).Fill(size);



            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (targetType.IsValueType ||
                targetType.IsClass || targetType.IsInterface)
                return Dictionary.GetComplexGenerator(targetType).Fill(size);

            else return null;
            
            
        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            Object[] obj =  Fill(rnd.Next(0, 50));
            return obj;
            

        }

       /* public new void SetValue(PropertyInfo p, Object target)
        {
            
            object [] o = Fill(rnd.Next(0, 50));
            
            p.SetValue(target, o);

        }

        public new void SetValue(FieldInfo f, Object target)
        {
            object[] o = Fill(rnd.Next());

            f.SetValue(target, o);

        }*/
    }
}