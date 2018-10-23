using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PrimitiveFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public PrimitiveFixture(Type t) : base(t)
        {

        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            object ret=null;
            if (t == typeof(int))
                ret = rnd.Next();

            else if (t == typeof(char))
                ret = RandomChar();

            else if (t == typeof(double))
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
