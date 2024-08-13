using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class MinimumLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        private int _minimumLength { get; set; }

        public MinimumLengthRule(int minimumLength)
        {
            this._minimumLength = minimumLength;
        }

        public bool Check(T value)
        {
            return (value as string)?.Length > _minimumLength;
        }
    }
}
