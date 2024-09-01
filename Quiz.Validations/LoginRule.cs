using Plugin.ValidationRules;
using Plugin.ValidationRules.Interfaces;
using Plugin.ValidationRules.Rules;

namespace Quiz.Validations
{
    public class LoginRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private Validatable<string> PseudoValidator;
        private Validatable<string> MailValidator;

        public LoginRule()
        {
            PseudoValidator = new Validatable<string>();
            PseudoValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a login" });
            PseudoValidator.Validations.Add(new MinimumLengthRule(4) { ValidationMessage = "You must provide a login that's at least 4 characters long." });
            PseudoValidator.Validations.Add(new MaximumLengthRule(20) { ValidationMessage = "You must provide a login should'nt be more than 20 characters long." });
            MailValidator = new Validatable<string>();
            MailValidator.Validations.Add(new IsNotNullOrEmptyRule<string>());
            MailValidator.Validations.Add(new EmailRule());
        }

        public bool Check(string value)
        {
            PseudoValidator.Value = value;
            MailValidator.Value = value;
            return PseudoValidator.Validate() || MailValidator.Validate() ? true : false;
        }
    }
}
