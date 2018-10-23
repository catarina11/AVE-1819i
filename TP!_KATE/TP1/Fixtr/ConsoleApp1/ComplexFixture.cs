using System;
using System.Reflection;

namespace ConsoleApp1
{
    public class ComplexFixture : GeneratorIFixture
    {
        private readonly IFixture fix;
        public ComplexFixture(Type t): base(t)
        {
            fix = Dictionary.GetFixture(t);

        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            return fix.New();
        }
    }
}