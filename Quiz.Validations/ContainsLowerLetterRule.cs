using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class ContainsLowerLetterRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        private int _charNumber { get; set; }
        private Regex _regex 
        {
            get =>new Regex(@"[a-z]{" + _charNumber + "}");
        }

        public ContainsLowerLetterRule(int charNumber)
        {
            this._charNumber = charNumber;
        }

        public bool Check(T value)
        {
            return _regex.IsMatch(value as string ?? "");
        }
    }
}
