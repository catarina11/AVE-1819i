using System;

namespace Fixtr
{
    public interface IFixture
    {
        Type TargetType{get;}
        Object New();
        Object[] Fill(int size);
    }
}
