using System;
using System.Reflection;

namespace ConsoleApp1
{
    internal class ComplexFixture : GeneratorIFixture
    {
        public ComplexFixture(Type t): base(t)
        {

        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            return new FixtureReflect(TargetType).New();
        }
    }
}