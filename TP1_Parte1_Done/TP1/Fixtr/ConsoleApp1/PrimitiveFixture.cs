using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{



    class PrimitiveFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public PrimitiveFixture(PropertyInfo p) : base(p){}

        public PrimitiveFixture(FieldInfo f) : base(f){}

        public override object New()
        {
            object ret=null;
            if (p.PropertyType == typeof(int))
                ret = rnd.Next();

            else if (p.PropertyType == typeof(char))
                ret = RandomChar();

            else if (p.PropertyType == typeof(double))
                ret = rnd.NextDouble();

            return ret;
        }

        private object RandomChar()
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            int val = rnd.Next(0, chars.Length - 1);
            return chars[val];
        }
    }
}
