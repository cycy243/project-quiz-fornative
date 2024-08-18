using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class MaximumLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        private int _maximumLength { get; set; }

        public MaximumLengthRule(int maximumLength)
        {
            this._maximumLength = maximumLength;
        }

        public bool Check(T value)
        {
            return value?.ToString()?.Length <= _maximumLength;
        }
    }
}
