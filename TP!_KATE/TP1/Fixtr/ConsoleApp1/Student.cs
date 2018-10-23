using System;

namespace ConsoleApp1
{
    public class Student
    {
        
       /* public Student(int nr, string name, School school)
        {
            this.Nr = nr;
            this.Name = name;
            this.School = school;
        }
        public Student()
        {

        }*/
       public Student(int nr, string name, School school, int[] array)
        {
            this.Nr = nr;
            this.Name = name;
            this.School = school;
            this.array = array;
        }

        [Validation("CheckNumberHas5Digits")]
        public int Nr { get; set; }
        public string Name { get; set; }
        public School School { get; set; }
        public Address Addr { get; set; }

        public string naturality;

        //Case eng
        public int[] array { get; set; }

        //Added field for part3 
        [Validation("CheckDateIsGreaterThan1980")]
        public DateTime BirthDate
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString();
        }
       
    }
}