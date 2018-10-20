using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class NumberValidator
    {
        public static bool IsValidNumber(Type studentType, int value)
        {

            /*verifica se a propriedade Nr tem o atibuto Validation*/
            var hasPropertyAttr = Attribute.IsDefined(studentType.GetProperty("Nr"), typeof(Validation));
           
            if (hasPropertyAttr)
                return Validation.NumberHas5Digits(value);

            else return false;
        }

    }
}
