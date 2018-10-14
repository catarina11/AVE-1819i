using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{
    class FixtureReflect : IFixture
    {
        private Type type;
        List<GeneratorIFixture> ConstructorParams;
        List<Box> MemberParams;
        public Type TargetType => type;
        Random rnd = new Random();

        public FixtureReflect(Type type)
        {
            this.type = type;
            ConstructorParams = new List<GeneratorIFixture>();
            MemberParams = new List<Box>();

            ConstructorInfo[] ctors = type.GetConstructors();
            // assuming class Student has two ctors and position 0 is ctor given
            ConstructorInfo ctor = ctors[rnd.Next(ctors.Length)];

            ParameterInfo[] ctorParameters = ctor.GetParameters();

            //bool notFound = false;

            foreach (ParameterInfo param in ctorParameters)
            {
                GeneratorIFixture g = MapType(param.ParameterType);
                ConstructorParams.Add(g);
            }
        }


        GeneratorIFixture MapType(Type paramType)
        {
            if (paramType.IsPrimitive)
                return new PrimitiveFixture(paramType);
            /*2º caso  - se é string*/
            else if (paramType == typeof(string))
                return new StringFixture(paramType);

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (paramType.IsValueType ||
                paramType.IsClass || paramType.IsInterface)
                return new ComplexFixture(paramType);

            /*4º caso - Array*/
            else if (paramType.IsArray)
                return new ArrayFixture(paramType);
            else return null;
        }



        public object[] Fill(int size)
        {
            object[] res = new object[size];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = this.New();
            }
            return res;
        }

        public object New()
        {
            object[] p = new object[ConstructorParams.Count];

            for (int i = 0; i < p.Length; i++)
                p[i] = ConstructorParams[i].New();

            Object obj = Activator.CreateInstance(type,p); //cria um Student => new Student: nr, name, school

            foreach(Box b in MemberParams)
                b.SetValue(obj);

            return obj;
        }

        public IFixture Member(string nm)
        {
            PropertyInfo p = type.GetProperty(nm);
            if (p != null)
            {
                GeneratorIFixture g = MapType(p.PropertyType);
                MemberParams.Add(new Box(p, g));
                return this;
            }
            FieldInfo f = type.GetField(nm);
            if (f != null)
            {
                GeneratorIFixture g = MapType(f.FieldType);
                MemberParams.Add(new Box(f, g));
                return this;
            }

            return this;
        }

        public IFixture Member(string name, params object[] pool)
        {
            
            object[] rand = new object [pool.Length];
            for (int i = 0; i < rand.Length; i++)
                rand[i] = rand[rnd.Next(0, pool.Length)];
            return this;
        }

        public IFixture Member(string name, IFixture fixt)
        {
            throw new NotImplementedException();
        }
    }
public class Box
{
    GeneratorIFixture g;
    PropertyInfo p;
    FieldInfo f;

    public Box(PropertyInfo p, GeneratorIFixture g) : this(g)
    {
        this.p = p;
        this.f = null;
    }
    public Box(FieldInfo f, GeneratorIFixture g) : this(g)
    {
        this.p = null;
        this.f = f;
    }
    public Box(GeneratorIFixture g)
    {
        this.g = g;
    }

    public void SetValue(Object target)
    {
        if (p != null)
            g.SetValue(p, target);
        else
            g.SetValue(f, target);
    }

}
}

    

