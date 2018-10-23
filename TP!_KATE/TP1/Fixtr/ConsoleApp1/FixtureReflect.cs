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

            Dictionary.Add(type, this);
        }


        GeneratorIFixture MapType(Type paramType)
        {
            if (paramType.IsArray)
                return Dictionary.GetArrayGenerator(paramType);

            if (paramType.IsPrimitive)
                return Dictionary.GetPrimitiveGenerator(paramType);
            
               
            
            /*2º caso  - se é string*/
            else if (paramType == typeof(string))
                return Dictionary.GetStringGenerator(paramType);
            
                

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (paramType.IsValueType ||
                paramType.IsClass || paramType.IsInterface)
                return Dictionary.GetComplexGenerator(paramType);

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
            object[] p = new object[ConstructorParams.Count]; //Cria um object[] com o tamanho do numero de params do ctor
            Object obj=null;
            Object[] auxObject;
            for (int i=0; i<p.Length; ++i)
            {
                GeneratorIFixture g = ConstructorParams[i];
                if (g.TargetType.IsArray)
                {
                    Object[] param = (Object[])g.New();
                    Array objAux = Array.CreateInstance(g.TargetType.GetElementType(), param.Length);
                    Array.Copy(param, objAux, param.Length);
                    p[i] = objAux;
                }
                else p[i] = g.New();

                /*IFixture fix;
                Type paramType = ConstructorParams[i].TargetType;

                //if (Dictionary.CacheMapp.TryGetValue(paramType, out fix)) //se existe no dicionário
                  //  p[i] = fix.New();
                //else
                //{
                    p[i] = ConstructorParams[i].New(); //p[][]
                    if (TargetType.IsArray)
                    {
                        //convert Object[][] to Object[]
                        auxObject = (Object[])p[i];
                        obj = Array.CreateInstance(TargetType.GetElementType(), auxObject.Length);
                        Array.Copy(auxObject, (Array)obj, auxObject.Length); //e.g.Int[] 
                        
                    }
                    else
                        obj = Activator.CreateInstance(type, p);
                //}*/
                   
            }
            obj = Activator.CreateInstance(type, p);
           
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

    

