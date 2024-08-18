using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class UserIsAtLeast13YearsOldRule : IValidationRule<DateOnly>
    {
        public string ValidationMessage { get; set; }

        public bool Check(DateOnly value)
        {
            var now = DateTime.Now;
            if(value.Year <= (now.Year - 13))
            {
                return value.Month <= now.Month && value.Day <= now.Day;
            }
            return false;
        }
    }
}
