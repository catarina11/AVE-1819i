using System.Reflection;

namespace ConsoleApp1
{
    internal class ComplexFixture : GeneratorIFixture
    {
        public ComplexFixture(PropertyInfo p) : base(p)
        {
        }

        public ComplexFixture(FieldInfo f) : base(f)
        {
        }

        public override object New()
        {
            return new FixtureReflect(TargetType).New();
        }
    }
}