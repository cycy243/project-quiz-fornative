using CommunityToolkit.Maui.Alerts;
using Microsoft.Extensions.Options;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;
using Quiz.ForNative.Components.Form;
using Quiz.Validations;
using Quiz.ViewModels.Interface;
using System.Linq;
using MinimumLengthRule = Quiz.Validations.MinimumLengthRule;

namespace Quiz.ForNative.Views.Auth;

public partial class RegisterView : ContentPage
{
    internal static string RouteName = "RegisterView";
    public Validatable<string> EmailValidator { get; private set; }
    public Validatable<string> PasswordValidator { get; private set; }
    public Validatable<string> NameValidator { get; private set; }
    public Validatable<string> FirstnameValidator { get; private set; }
    public Validatable<DateOnly> BirthdateValidator { get; private set; }
    public Validatable<string> BioValidator { get; private set; }
    public Validatable<string> PseudoValidator { get; private set; }
    public string AvatarPath { get; private set; }
    public IRegisterViewModel ViewModel { get; private init; }

    public RegisterView(IRegisterViewModel viewModel)
	{
		InitializeComponent();

        ViewModel = viewModel;

        InitializeValidator();
        InitializedValidationHandlers();
    }

    private void OnRegister_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if(ALlFieldAreValid())
            {
                ErrorLabel.IsVisible = false;
                ViewModel.RegisterUser(new RegisterUserArgs(
                    NameValidator.Value,
                    FirstnameValidator.Value,
                    PseudoValidator.Value,
                    PasswordValidator.Value,
                    EmailValidator.Value,
                    AvatarPath,
                    BioValidator.Value,
                    BirthdateValidator.Value.ToString("dd/MM/yyyy")
                ));
            }
            else
            {
                ErrorLabel.IsVisible = true;
            }
        }
        catch(Exception ex)
        {
            Toast.Make(ex.Message).Show();
        }
    }

    private bool ALlFieldAreValid()
    {
        return EmailValidator.Validate() && PasswordValidator.Validate() && NameValidator.Validate() && FirstnameValidator.Validate()
            && PseudoValidator.Validate() && BioValidator.Validate() && BirthdateValidator.Validate();
    }

    private void InitializedValidationHandlers()
    {

        MailInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, EmailValidator);
        PasswordInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, PasswordValidator);
        LastnameInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, NameValidator);
        FirstnameInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, FirstnameValidator);
        PseudoInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, PseudoValidator);
        BioInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, BioValidator);
        BirthdateInput.ValidationFunction += (inputName, value) => ValidateInput(DateOnly.Parse((value as string)!), BirthdateValidator);
        ConfirmationPasswordInput.ValidationFunction += (inputName, value) => {
            return value != null && (value as string) == PasswordValidator.Value ? "" : "The passwords doesn't match";
        };
        AvatarInput.ValidationFunction += (inputName, value) =>
        {
            string castedValue = value as string;
            if (castedValue != null && FileExtensionIsValid(castedValue))
            {

                AvatarPath = castedValue;
                return "";
            }
            return "Only accept file with type: jpg, jpeg and png";
        };
    }

    private bool FileExtensionIsValid(string filePath)
    {
        return filePath.Contains(".jpg") || filePath.Contains(".jpeg") || filePath.Contains(".png");
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
        PasswordValidator.Validations.Add(new MinimumLengthRule(8) { ValidationMessage = "The password must have a minimum length of 8" });
        PasswordValidator.Validations.Add(new ContainsUpperLetterRule(1) { ValidationMessage = "Your password must contains at least one upper character" });
        PasswordValidator.Validations.Add(new ContainsLowerLetterRule(1) { ValidationMessage = "Your password must contains at least one lower character" });
        NameValidator = new Validatable<string>();
        NameValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a name" });
        NameValidator.Validations.Add(new MinimumLengthRule(3) { ValidationMessage = "You must provide a name that's at least 3 characters long." });
        FirstnameValidator = new Validatable<string>();
        FirstnameValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a firstname" });
        FirstnameValidator.Validations.Add(new MinimumLengthRule(3) { ValidationMessage = "You must provide a firstname that's at least 3 characters long." });
        BioValidator = new Validatable<string>();
        BioValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a bio" });
        BioValidator.Validations.Add(new MinimumLengthRule(15) { ValidationMessage = "You must provide a bio that's at least 15 characters long." });
        BioValidator.Validations.Add(new MaximumLengthRule(255) { ValidationMessage = "You must provide a bio should'nt be more than 255 characters long." });
        PseudoValidator = new Validatable<string>();
        PseudoValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a pseudo" });
        PseudoValidator.Validations.Add(new MinimumLengthRule(4) { ValidationMessage = "You must provide a pseudo that's at least 4 characters long." });
        PseudoValidator.Validations.Add(new MaximumLengthRule(20) { ValidationMessage = "You must provide a pseudo should'nt be more than 20 characters long." });
        BirthdateValidator = new Validatable<DateOnly>();
        BirthdateValidator.Validations.Add(new UserIsAtLeast13YearsOldRule());
    }
}