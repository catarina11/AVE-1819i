using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Validation : Attribute
    {
        private string v;

        public Validation(string v)
        {
            this.v = v;
        }

        //verifca se valor passado contém 5 digitos
        public static bool NumberHas5Digits(int number)
        {
            int size  = number.ToString().Length;
            return size == 5;

        }

        //verifca se a data é superior a 1980
        public static bool DateIsGreaterThan1980(DateTime dt)
        {
            return dt.Year > 1980;

        }
    }
}
