using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MemberArrayGeneratorFixture : GeneratorIFixture
    {
        Random rnd = new Random();
        public MemberArrayGeneratorFixture(Type t) : base(t)
        {

        }
        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            throw new NotImplementedException();
        }

        private object generateValueRandomOfArray(object[] pool)
        {
            //gerar valor random do array pool
            Object obj = new object();
            obj = pool[rnd.Next(0, pool.Length)];
            return obj;
        }
    }
}
