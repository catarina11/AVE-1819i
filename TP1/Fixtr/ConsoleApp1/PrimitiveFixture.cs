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
            return rnd.Next();
        }
    }
}
