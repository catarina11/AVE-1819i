using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IFixture
    {
        Type TargetType { get; }
        Object New();
        Object[] Fill(int size);
    }
}
