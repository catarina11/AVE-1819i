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
            //IFixture fix = new FixtureReflect(typeof(Student));
            //Student s1 = (Student)fix.New();
            //Student s2 = (Student)fix.New();
            IFixture fix = new FixtureReflect(typeof(Student)).Member("Address").Member("naturality");
            Student s1 = (Student)fix.New();
        }
    }

   
}
