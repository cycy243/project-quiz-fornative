using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;

namespace Quiz.ForNative.Views.Auth;

public partial class RegisterView : ContentPage
{

    public RegisterView()
	{
		InitializeComponent();

		InitializedValidationHandlers();
    }

	private void InitializedValidationHandlers()
    {

        var EmailValidator = Validator.Build<string>()
            .WithRule(new EmailRule(), "Email is not valide");


        MailInput.ValidationFunction += (inputName, value) =>
        {
            EmailValidator.Value = value;
            var errors = EmailValidator.Validate();
            return EmailValidator.Error;
        };
    }
}