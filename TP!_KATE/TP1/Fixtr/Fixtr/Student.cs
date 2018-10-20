using System;
using System.Collections.Generic;
using System.Text;

namespace Fixtr
{
    class Student
    {
        public Student(int nr, string name, School school)
        {
            this.Nr = nr;
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
