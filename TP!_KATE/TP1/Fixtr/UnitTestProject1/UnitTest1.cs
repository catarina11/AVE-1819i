using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestStudentNotNull()
        {
            IFixture fix = new FixtureReflect(typeof(Student));
            Student s1 = (Student)fix.New();
            Assert.IsNotNull(s1);
        }
        [TestMethod]
        public void TestPart2Struct()
        {
            IFixture newFix = new FixtureReflect(typeof(Student)).Member("Addr").Member("naturality");
            Student s2 = (Student)newFix.New();
            Assert.IsNotNull(s2);
            Assert.IsNotNull(s2.Addr.Nr);
            Assert.IsNotNull(s2.Addr.Street);
            Assert.IsNotNull(s2.Addr.PostalCode);
        }

        [TestMethod]
        public void TestMemberPart3()
        {
            IFixture fix2 = new FixtureReflect(typeof(Student))
                .Member("Name", "Maria Papoila", "Augusto Seabra")
                .Member("Nr", 8713, 2312, 23123, 131, 54534)
                .Member("BirthDate", new FixtureDateTime());
            Student s3 = (Student)fix2.New();

            Assert.IsNotNull(s3);
            Assert.IsNotNull(s3.Name);
            Assert.IsNotNull(s3.Nr);
            Assert.IsNotNull(s3.BirthDate);
    
        }

        [TestMethod]
        public void TestPart4NumberIsValid()
        {
            bool validArg = NumberValidator.IsValidNumber(typeof(Student), 11111); // true
            Assert.IsTrue(validArg);
        }

        [TestMethod]
        public void TestPart4NumberIsNotValid()
        {
            bool validArg = NumberValidator.IsValidNumber(typeof(Student), 2500); // false
            Assert.IsFalse(validArg);
        }

        [TestMethod]
        public void TestPart4DateIsValid()
        {
            bool validArg = DateValidator.IsValidDate(typeof(Student), new DateTime(2020, 01, 01)); //true
            Assert.IsTrue(validArg);
        }

        [TestMethod]
        public void TestPart4DateIsNotValid()
        {
            bool validArg = DateValidator.IsValidDate(typeof(Student), new DateTime(1971, 01, 01)); //fals
            Assert.IsFalse(validArg);
        }

        


    }
}
