using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class MaximumLengthRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        private int _maximumLength { get; set; }

        public MaximumLengthRule(int maximumLength)
        {
            if(maximumLength <= 0)
            {
                throw new ArgumentException("You shouldn't set a maximum length equal or less than 0");
            }
            this._maximumLength = maximumLength;
        }

        public bool Check(string value)
        {
            if (value is null) return false;
            return value?.ToString()?.Length <= _maximumLength;
        }
    }
}
