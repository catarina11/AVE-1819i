using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FixtureDateTime : IFixture {
        Random rand = new Random();
        DateTime dt = new DateTime(1970, 1, 1);
        public Type TargetType
        {
            get { return typeof(DateTime); }
        }

        public object New()
        {
            return dt.AddMonths(rand.Next(600));
        }

        public object[] Fill(int size)
        {
            object[] arr = new object[size];
            for (int i = 0; i < size; i++)
                arr[i] = New();
            return arr;
        }
    }
}
