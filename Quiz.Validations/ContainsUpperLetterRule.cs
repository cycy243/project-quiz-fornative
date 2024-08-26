﻿using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz.Validations
{
    public class ContainsUpperLetterRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private int _charNumber { get; set; }
        private Regex _regex
        {
            get => new Regex(@"[A-Z]");
        }

        public ContainsUpperLetterRule(int charNumber)
        {
            this._charNumber = charNumber;
        }

        public bool Check(string value)
        {
            if (value is null) return false;
            int count = _regex.Matches(value).Count;
            return _charNumber == 0 ? count == 0 : count >= _charNumber;
        }
    }
}
