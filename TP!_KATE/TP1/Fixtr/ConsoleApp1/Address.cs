using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public struct Address
    {
        public string Street;
        public string PostalCode;
        public int Nr;

        public Address(string street, string postalCode, int nr)
        {
            this.Street = street;
            this.PostalCode = postalCode;
            this.Nr = nr;
        }
    }

    /*public class Address
    {
        public Address()
        {

        }
        public Address(string addr, string code, string local)
        {
            this.Addr = addr;
            this.Code = code;
            this.Local = local;
        }
        
        public string Addr { get; set; }
        public string Code { get; set; }

        public string Local { get; set; }

        public string Country;
          
    }*/
}
