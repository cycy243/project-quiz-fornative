using Microsoft.Extensions.Options;
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
    Validatable<string> NameValidator { get; set; }
    Validatable<string> FirstnameValidator { get; set; }
    Validatable<DateOnly> BirthdateValidator { get; set; }
    Validatable<string> BioValidator { get; set; }
    Validatable<string> PseudoValidator { get; set; }

    public RegisterView()
	{
		InitializeComponent();

        InitializeValidator();
        InitializedValidationHandlers();
    }

	private void InitializedValidationHandlers()
    {

        MailInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, EmailValidator);
        PasswordInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, PasswordValidator);
        LastnameInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, NameValidator);
        FirstnameInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, FirstnameValidator);
        PseudoInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, PseudoValidator);
        BioInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, BioValidator);
        ConfirmationPasswordInput.ValidationFunction += (inputName, value) => {
            return value != null && (value as string) == PasswordValidator.Value ? "" : "The passwords doesn't match";
        };
    }

    private string ValidateInput<S>(S input, Validatable<S> validator)
    {
        validator.Value = input == null ? default : input;
        var errors = validator.Validate();
        return validator.Error;
    }

    private void InitializeValidator()
    {
        EmailValidator = Validator.Build<string>()
            .WithRule(new EmailRule(), "Email is not valide");
        PasswordValidator = new Validatable<string>();
        PasswordValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a password" });
        PasswordValidator.Validations.Add(new MinimumLengthRule<string>(8) { ValidationMessage = "The password must have a minimum length of 8" });
        PasswordValidator.Validations.Add(new ContainsUpperLetterRule(1) { ValidationMessage = "Your password must contains at least one upper character" });
        PasswordValidator.Validations.Add(new ContainsLowerLetterRule(1) { ValidationMessage = "Your password must contains at least one lower character" });
        NameValidator = new Validatable<string>();
        NameValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a name" });
        NameValidator.Validations.Add(new MinimumLengthRule<string>(3) { ValidationMessage = "You must provide a name that's at least 3 characters long." });
        FirstnameValidator = new Validatable<string>();
        FirstnameValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a firstname" });
        FirstnameValidator.Validations.Add(new MinimumLengthRule<string>(3) { ValidationMessage = "You must provide a firstname that's at least 3 characters long." });
        BioValidator = new Validatable<string>();
        BioValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a bio" });
        BioValidator.Validations.Add(new MinimumLengthRule<string>(15) { ValidationMessage = "You must provide a bio that's at least 15 characters long." });
        BioValidator.Validations.Add(new MaximumLengthRule<string>(255) { ValidationMessage = "You must provide a bio should'nt be more than 255 characters long." });
        PseudoValidator = new Validatable<string>();
        PseudoValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a pseudo" });
        PseudoValidator.Validations.Add(new MinimumLengthRule<string>(4) { ValidationMessage = "You must provide a pseudo that's at least 4 characters long." });
        PseudoValidator.Validations.Add(new MaximumLengthRule<string>(20) { ValidationMessage = "You must provide a pseudo should'nt be more than 20 characters long." });
    }
}