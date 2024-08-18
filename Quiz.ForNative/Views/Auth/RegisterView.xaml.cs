using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;
using Quiz.ForNative.Components.Form;
using Quiz.Validations;

namespace Quiz.ForNative.Views.Auth;

public partial class RegisterView : ContentPage
{
    Validatable<string> EmailValidator { get; set; }
    Validatable<string> PasswordValidator { get; set; }

    public RegisterView()
	{
		InitializeComponent();

        InitializeValidator();
        InitializedValidationHandlers();
    }

	private void InitializedValidationHandlers()
    {

        MailInput.ValidationFunction += (inputName, value) =>
        {
            var mail = value as string;
            EmailValidator.Value = mail == null ? "" : mail;
            var errors = EmailValidator.Validate();
            return EmailValidator.Error;
        };
        PasswordInput.ValidationFunction += (inputName, value) =>
        {
            var pwd = value as string;
            PasswordValidator.Value = pwd == null ? "" : pwd;
            var errors = PasswordValidator.Validate();
            return PasswordValidator.Error;
        };
    }

    private void InitializeValidator()
    {
        EmailValidator = Validator.Build<string>()
            .WithRule(new EmailRule(), "Email is not valide");
        PasswordValidator = new Validatable<string>();
        PasswordValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a password" });
        PasswordValidator.Validations.Add(new MinimumLengthRule<string>(8) { ValidationMessage = "The password must have a minimum length of 8" });
        PasswordValidator.Validations.Add(new ContainsUpperLetterRule<string>(1) { ValidationMessage = "Your password must contains at least one upper character" });
        PasswordValidator.Validations.Add(new ContainsLowerLetterRule<string>(1) { ValidationMessage = "Your password must contains at least one lower character" });
    }
}