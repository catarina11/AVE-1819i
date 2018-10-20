using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{
    public class FixtureReflect : IFixture
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

            ConstructorInfo ctor;
            ConstructorInfo[] ctors = type.GetConstructors();
            ctor = ctors[rnd.Next(ctors.Length)];

            ParameterInfo[] ctorParameters = ctor.GetParameters();

            foreach (ParameterInfo param in ctorParameters)
            {
                GeneratorIFixture g = MapType(param.ParameterType);
                ConstructorParams.Add(g);
            }
        }


        GeneratorIFixture MapType(Type paramType)
        {
            if (paramType.IsPrimitive)
            {
                /*4º caso - Array*/
                if (paramType.IsArray)
                    return new ArrayFixture(paramType);
                else
                    return new PrimitiveFixture(paramType);
            }
               
            
            /*2º caso  - se é string*/
            else if (paramType == typeof(string))
            {
                if (paramType.IsArray)
                    return new ArrayFixture(paramType);
                else
                    return new StringFixture(paramType);
            }
                

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (paramType.IsValueType ||
                paramType.IsClass || paramType.IsInterface)
                return new ComplexFixture(paramType);

            

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
            
           Type t = TargetType;
           IFixture fixture;
           object[] p = new object[ConstructorParams.Count]; //Cria um object[] com o tamanho do numero de params do ctor

            for(int i=0; i<p.Length; ++i)
            {
                IFixture fix;
                Type paramType = ConstructorParams[i].TargetType;
                
                if (Dictionary.CacheMapp.TryGetValue(paramType, out fix))
                    p[i] = fix.New();
                else
                    p[i] = ConstructorParams[i].New();
            }
            Object obj = null;
            if (t.IsArray)
                obj = Array.CreateInstance(TargetType, p.Length);
            
            else
                obj = Activator.CreateInstance(type,p);
            if (Dictionary.CacheMapp.TryGetValue(TargetType, out fixture))
                Dictionary.CacheMapp[TargetType] = this;
            else
                Dictionary.CacheMapp.Add(TargetType, this);

            foreach (Box b in MemberParams)
            {
                b.SetValue(obj);
            }
            return obj;

        }

        public IFixture Member(string nm)
        {
            PropertyInfo p = type.GetProperty(nm);
            if (p != null)
            {
                GeneratorIFixture g = MapType(p.PropertyType);
                MemberParams.Add(new Box(p, g, null, null));
                return this;
            }
            FieldInfo f = type.GetField(nm);
            if (f != null)
            {
                GeneratorIFixture g = MapType(f.FieldType);
                MemberParams.Add(new Box(f, g, null, null));
                return this;
            }

            return this;
        }

        public IFixture Member(string name, params object[] pool)
        {
            PropertyInfo p = type.GetProperty(name);
            if (p != null)
            {
                GeneratorIFixture g = MapType(p.PropertyType);
                MemberParams.Add(new Box(p, g, pool, null));
                return this;
            }
            FieldInfo f = type.GetField(name);
            if (f != null)
            {
                GeneratorIFixture g = MapType(f.FieldType);
                MemberParams.Add(new Box(f, g, pool, null));
                return this;
            }


            return this;
        }

       
        public IFixture Member(string name, IFixture fixt)
        {
            
            PropertyInfo p = type.GetProperty(name);
            if (p != null)
            {
                if (p.PropertyType.Equals(fixt.TargetType))
                {
                    GeneratorIFixture g = MapType(p.PropertyType);
                    MemberParams.Add(new Box(p, g, null, fixt));
                }
                else
                    throw new Exception("Member not compatible to IFixture");
                
            }
            FieldInfo f = type.GetField(name);
            if (f != null)
            {
                if (p.PropertyType.Equals(fixt.TargetType))
                {
                    GeneratorIFixture g = MapType(f.FieldType);
                    MemberParams.Add(new Box(f, g, null, fixt));
                }
                else
                    throw new Exception("Member not compatible to IFixture");
            }
            return this;
        }
    }
public class Box
{
    GeneratorIFixture g;
    PropertyInfo p;
    FieldInfo f;
    //Added for Part 3
    Object[] obj;
    IFixture fixt;

    public Box(PropertyInfo p, GeneratorIFixture g, Object[] obj, IFixture fixt) : this(g)
    {
        this.p = p;
        this.f = null;
        this.obj = obj; //parte 3 array random
        this.fixt = fixt;
    }
    public Box(FieldInfo f, GeneratorIFixture g, Object[] obj, IFixture fixt) : this(g)
    {
        this.p = null;
        this.f = f;
        this.obj = obj;
        this.fixt = fixt;
    }
    public Box(GeneratorIFixture g)
    {
        this.g = g;
    }

    public void SetValue(Object target)
    {
            if (p != null)
                g.SetValue(p, target, obj, fixt);
            else if (f != null && obj == null)
                g.SetValue(f, target, obj, fixt);
        }
    }
}

    

