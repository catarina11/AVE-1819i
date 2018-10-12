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
        List<GeneratorIFixture> attrsToGenerate;
        public FixtureReflect(Type type)
        {
            this.type = type;
            attrsToGenerate = new List<GeneratorIFixture>();
            foreach (PropertyInfo prop in type.GetProperties())
                MapProperty(prop);

            foreach (FieldInfo fld in type.GetFields())
                MapField(fld);  

        }

        public FixtureReflect Member(string v)
        {
            string x;
            foreach (PropertyInfo prop in type.GetProperties())
                if (prop.Name == v)
                    MapProperty(prop);


            foreach (FieldInfo fld in type.GetFields())
                if (fld.Name == v)
                    MapField(fld);


            return new FixtureReflect(type);
        }

        void MapProperty(PropertyInfo prop)
        {
            //verifcar que tipo é Student

            /*1º caso - se é do tipo primitivo*/
            if (prop.PropertyType.IsPrimitive)
                attrsToGenerate.Add(new PrimitiveFixture(prop));

            /*2º caso  - se é string*/
            else if (prop.PropertyType == typeof(string))
            {
                attrsToGenerate.Add(new StringFixture(prop));
            }

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (prop.PropertyType.IsValueType ||
                prop.PropertyType.IsClass || prop.PropertyType.IsInterface)
                attrsToGenerate.Add(new ComplexFixture(prop));

            /*4º caso - Array*/
            else if (prop.PropertyType.IsArray)
            {
                attrsToGenerate.Add(new ArrayFixture(prop));
            }
        }
        void MapField(FieldInfo fld)
        {
            //verifcar que tipo é Student

            /*1º caso - se é do tipo primitivo*/
            if (fld.FieldType.IsPrimitive)
                attrsToGenerate.Add(new PrimitiveFixture(fld));

            /*2º caso  - se é string*/
            else if (fld.FieldType == typeof(string))
                attrsToGenerate.Add(new StringFixture(fld));

            /*3º caso - se é complexo (referencia ou valor)*/
            //Tipo valor - struct, enum, bool
            else if (fld.FieldType.IsValueType ||
                fld.FieldType.IsClass || fld.FieldType.IsInterface)
                attrsToGenerate.Add(new ComplexFixture(fld));

            /*4º caso - Array*/
            else if (fld.FieldType.IsArray)
                attrsToGenerate.Add(new ArrayFixture(fld));
        }

        public Type TargetType => type;

        public object[] Fill(int size)
        {
            object[] res = new object[size];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = New();
            }
            return res;
        }

        public object New()
        {
            Object obj = Activator.CreateInstance(type); //cria um Student => new Student: nr, name, school
            foreach (GeneratorIFixture item in attrsToGenerate)
            {
                item.SetValue(obj);
           
            }
            return obj;
        }

        


    }
}
