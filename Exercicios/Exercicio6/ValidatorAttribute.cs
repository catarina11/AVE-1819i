using System;

namespace Exercicios
{
    internal class ValidatorAttribute : Attribute
    {
        private string v;

        public ValidatorAttribute(string v)
        {
            this.v = v;
        }

        //verifca se valor passado como parametros é superior a 20000
        public static bool NumberGreaterThan20000(int number)
        {
           
            return number>20000?  true : false;

        }
    }
}