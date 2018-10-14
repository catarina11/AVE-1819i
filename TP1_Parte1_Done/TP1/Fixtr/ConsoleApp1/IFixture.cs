using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IFixture
    {
        Type TargetType { get; }
        Object New();
        Object[] Fill(int size);
        IFixture Member(string v);

        IFixture Member(string name, params object[] pool);

        IFixture Member(string name, IFixture fixt);
        

        
    }
}
