using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Utils
{
    internal class FormValidator
    {
        private string Field { get; set; }
        private string[] Rules { get; set; }

        public FormValidator(string fieldName, string fieldRules) 
        {
            Field = fieldName;
            Rules = fieldRules.Split('|');
        }

        public IEnumerable<string> ValidateInput(string input)
        {
            List<string> result = new List<string>();
            foreach (var rule in Rules)
            {
                var error = ValidateInputWithRule(Field, rule, input);
                if (error != string.Empty)
                {
                    result.Add(error);
                }
            }
            return result;
        }

        private static string ValidateInputWithRule(string field, string input, string rule)
        {
            var items = rule.Split(":");
            var constraint = items.Length == 1 ? "" : items[1];
            switch (rule)
            {
                case "required":
                    return input.Length == 0 || string.IsNullOrWhiteSpace(input) ? $"The {field} is required" : string.Empty;
                case "max":
                    return input.Length < int.Parse(constraint) ? string.Empty : $"The {field} shouldn't have more than {constraint} characters";
                case "min":
                    return input.Length > int.Parse(constraint) ? string.Empty : $"The {field} shouldn't have less than {constraint} characters";
                default:
                    return string.Empty;
            }
        }
    }

    internal class FormValidatorFactory
    {
        private Dictionary<string, string> FieldsRules = new Dictionary<string, string>();

        public FormValidatorFactory(Dictionary<string, string> fieldsRules)
        {
            FieldsRules = fieldsRules;
        }

        /// <summary>
        /// Create and return a validator for a given field
        /// </summary>
        /// <param name="field">field the validator will validate</param>
        /// <returns>
        /// Return the validator.
        /// </returns>
        public FormValidator CreateValidator(string field)
        {
            if(field == null)
            {
                throw new ArgumentNullException("The [field] argument must be a valid string");
            }
            return null;
        }
    }
}
