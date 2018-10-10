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
        private Random rnd = new Random();
        private Type type;
       
        public FixtureReflect(Type type)
        {
            this.type = type;
        }

        public Type TargetType => type;

        public object[] Fill(int size)
        {
            return null;
        }

        public object New()
        {
            Type j;
            int randomValue;
            string str;
            Object objRef;
            PropertyInfo[] propsInfos = type.GetProperties();
            foreach(PropertyInfo prop in propsInfos)
            {
                //verifcar que tipo é Student

                /*1º caso - se é do tipo primitivo*/
                if (prop.PropertyType.IsPrimitive)
                    randomValue = GenerateRandomValue();
                /*2º caso  - se é string*/
                else if (prop.PropertyType == typeof(string))
                {
                    str = GenerateString(rnd.Next(0, 50)); //gera um valor aleatorio no max até 50
                }

                /*3º caso - se é complexco (referencia ou valor)*/
                //Tipo valor - struct, enum, bool
                else if (prop.PropertyType.IsValueType ||
                    prop.PropertyType.IsClass || prop.PropertyType.IsInterface)
                    objRef = Activator.CreateInstance(prop.PropertyType);
                
                /*4º caso - Array*/
                //else if (prop.PropertyType.IsArray)
                //do stuff
            }

            Object obj = Activator.CreateInstance(type, new object[3]); //cria um Student => new Student: nr, name, school
            return obj;
        }

        private string GenerateString(int length)
        {
            //gera uma string aleatoria
            const string alfa = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
            char[] randomChar = new char[length];
            for (int i = 0; i < length; i++)
                randomChar[i] = alfa[rnd.Next(0, alfa.Length)];
            return new string(randomChar);


        }

        public int GenerateRandomValue()
        {
            //gerar um valor aleatorio
            
            return rnd.Next();
        }
    }
}
