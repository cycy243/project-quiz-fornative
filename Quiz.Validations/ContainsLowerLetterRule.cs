using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class ContainsLowerLetterRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private int _charNumber { get; set; }
        private Regex _regex 
        {
            get =>new Regex(@"[a-z]");
        }

        public ContainsLowerLetterRule(int charNumber)
        {
            this._charNumber = charNumber;
        }

        public bool Check(string value)
        {
            int count = _regex.Matches(value).Count;
            return _charNumber == 0 ? count == 0 : count >= _charNumber;
        }
    }
}
