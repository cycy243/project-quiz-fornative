using Plugin.ValidationRules.Rules;
using Plugin.ValidationRules;
using Quiz.Validations;
using MinimumLengthRule = Quiz.Validations.MinimumLengthRule;

namespace Quiz.ForNative.Views.Auth;

public partial class LoginView : ContentPage
{
    private Validatable<string> PasswordValidator;
    private Validatable<string> LoginValidator;

    public LoginView()
    {
        InitializeComponent();

        InitializeValidator();
        InitializedValidationHandlers();
    }

    private async void OnLogin_Clicked(object? sender, EventArgs e)
	{
        if (ALlFieldAreValid())
        {
            ErrorLabel.IsVisible = false;
        }
        else
        {
            ErrorLabel.IsVisible = true;
        }
    }

    private bool ALlFieldAreValid()
    {
        bool validationsResult = LoginInput.Validate();
        validationsResult &= PasswordInput.Validate();
        return validationsResult;
    }

    private void InitializedValidationHandlers()
    {

        LoginInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, LoginValidator);
        PasswordInput.ValidationFunction += (inputName, value) => ValidateInput((value as string)!, PasswordValidator);
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
        PasswordValidator = new Validatable<string>();
        PasswordValidator.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You must provide a password" });
        PasswordValidator.Validations.Add(new MinimumLengthRule(8) { ValidationMessage = "The password must have a minimum length of 8" });
        PasswordValidator.Validations.Add(new ContainsUpperLetterRule(1) { ValidationMessage = "Your password must contains at least one upper character" });
        PasswordValidator.Validations.Add(new ContainsLowerLetterRule(1) { ValidationMessage = "Your password must contains at least one lower character" });
        LoginValidator = new Validatable<string>();
        LoginValidator.Validations.Add(new LoginRule { ValidationMessage = "You must provide a name" });
    }
}