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
            IFixture fix = new FixtureReflect(typeof(Student));
            Student s1 = (Student)fix.New();

            //Parte 2
            IFixture newFix = new FixtureReflect(typeof(Student)).Member("Addr").Member("naturality");
            Student s2 = (Student)newFix.New();

            //Parte 3
            IFixture fix2 = new FixtureReflect(typeof(Student))
                .Member("Name", "Maria Papoila", "Augusto Seabra")
                .Member("Nr", 8713, 2312, 23123, 131, 54534)
                .Member("BirthDate", new FixtureDateTime());
            Student s3 = (Student)fix2.New();

            //Parte 4
            //Teste NR
            bool validArg = NumberValidator.IsValidNumber(typeof(Student), 11111); // true
            validArg = NumberValidator.IsValidNumber(typeof(Student), 2500); // false
           
            //Teste Date
            validArg = DateValidator.IsValidDate(typeof(Student), new DateTime(2020,01,01)); //true
            validArg = DateValidator.IsValidDate(typeof(Student), new DateTime(1971, 01, 01)); //false
            var x = 0;

        }
    }

   
}
