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
            //Parte 1
            //IFixture fix = new FixtureReflect(typeof(Student));
            //Student s1 = (Student)fix.New();
           
            //Parte 2
            //IFixture newFix = new FixtureReflect(typeof(Student)).Member("Addr").Member("naturality");
            //Student s2 = (Student)newFix.New();

            //Parte 3
            IFixture fix2 = new FixtureReflect(typeof(Student))
                .Member("Name", "Maria Papoila", "Augusto Seabra")
                .Member("Nr", 8713, 2312, 23123, 131, 54534);
                //.Member("BirthDate", new FixtureDateTime());
           Student s3 = (Student)fix2.New();

            var x = 0;
        }
    }

   
}
