using System;

namespace Fixtr
{
    internal class FixtureReflect : IFixture
    {
        private Type type;

        public FixtureReflect(Type type)
        {
            this.type = type;
        }

        public Type TargetType => throw new NotImplementedException();

        public object[] Fill(int size)
        {
            throw new NotImplementedException();
        }

        public object New()
        {
            Object obj = Activator.CreateInstance(type); //cria um Student => new Student: nr, name, school
            return obj;
        }
    }
}