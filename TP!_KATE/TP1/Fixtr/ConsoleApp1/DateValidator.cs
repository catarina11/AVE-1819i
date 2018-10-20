using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class DateValidator
    {
        public static bool IsValidDate(Type studentType, DateTime dt)
        {
            /*verifica se a propriedade BirthDate tem o atibuto Validation*/
            var hasPropertyAttr = Attribute.IsDefined(studentType.GetProperty("BirthDate"), typeof(Validation));

            if (hasPropertyAttr)
                return Validation.DateIsGreaterThan1980(dt);

            else return false;


        }
    }
}
