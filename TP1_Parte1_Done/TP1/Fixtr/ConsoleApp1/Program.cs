using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            IFixture fix = new FixtureReflect(typeof(Student));
            Student s1 = (Student)fix.New();
           
            IFixture newFix = new FixtureReflect(typeof(Student)).Member("Addr").Member("naturality");
            Student s2 = (Student)newFix.New();
            var x = 0;
        }
    }

   
}
