namespace ConsoleApp1
{
    public class Student
    {
       
        public Student(int nr, string name, School school)
        {
            this.Nr = nr;
            this.Name = name;
            this.School = school;
        }
        public Student()
        {

        }
        public int Nr { get; set; }
            public string Name { get; set; }
            public School School { get; set; }
            public Address Addr { get; set; }
            public string naturality;

            public override string ToString()
            {
                return base.ToString();
            }


       
    }


}