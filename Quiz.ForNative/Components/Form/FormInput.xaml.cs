using System.ComponentModel.DataAnnotations;

namespace Quiz.ForNative.Components.Form;

public partial class FormInput : ContentView
{
    #region [BindableProperties]
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(FormInput),
        default(string));
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(FormInput),
        default(string));
    public static readonly BindableProperty ValidationRulesProperty = BindableProperty.Create(
        nameof(ValidationRules),
        typeof(string),
        typeof(FormInput),
        default(string));
    public static readonly BindableProperty ValidationFunctionProperty = BindableProperty.Create(
        nameof(ValidationFunction),
        typeof(InputValidationFunction),
        typeof(FormInput));
    public static readonly BindableProperty InputNameProperty = BindableProperty.Create(
        nameof(InputName),
        typeof(string),
        typeof(FormInput),
        default(string));
    public static readonly BindableProperty ErrorTxtProperty = BindableProperty.Create(
        nameof(ErrorTxt),
        typeof(string),
        typeof(FormInput),
        default(string));
    #endregion
    #region [Properties]
    public string LabelContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Placeholder
    {
        get => GetValue(PlaceholderProperty) as string;
        set => SetValue(PlaceholderProperty, value);
    }
    public InputValidationFunction ValidationFunction
    {
        get => GetValue(ValidationFunctionProperty) as InputValidationFunction;
        set => SetValue(ValidationFunctionProperty, value);
    }
    public string InputName
    {
        get => GetValue(InputNameProperty) as string;
        set => SetValue(InputNameProperty, value);
    }
    public string ValidationRules
    {
        get => GetValue(ValidationRulesProperty) as string;
        set => SetValue(ValidationRulesProperty, value);
    }
    public string ErrorTxt
    {
        get => ErrorLabel != null ? ErrorLabel.Text : string.Empty;
        set
        {
            ErrorLabel.Text = string.IsNullOrEmpty(value) ? string.Empty : value;
            ErrorLabel.IsVisible = !string.IsNullOrEmpty(value);
        }
    }
    public InputType TypeInput { get; set; }
    public string Value { get; set; }
    private Label ErrorLabel;
    #endregion

    public FormInput()
	{
		InitializeComponent();
    }

    protected override void OnParentSet()
    {
        InitChild();
    }

    private void InitChild()
    {
        View inputField = null;
        var errorLabel = new Label { IsVisible = false, WidthRequest = 250 };
        switch(TypeInput)
        {
            case InputType.TextArea:
                inputField = new Editor();
                inputField.Unfocused += (sender, args) =>
                {
                    Value = (sender as Editor).Text;
                };
                inputField.SetBinding(Editor.PlaceholderProperty, new Binding("Placeholder"));
                AddValidationHandler(inputField, errorLabel);
                break;
            case InputType.Password:
                inputField = new Entry();
                inputField.Unfocused += (sender, args) =>
                {
                    Value = (sender as Entry).Text;
                };
                (inputField as Entry).IsPassword = true;
                inputField.SetBinding(Editor.PlaceholderProperty, new Binding("Placeholder"));
                AddValidationHandler(inputField, errorLabel);
                break;
            case InputType.Date:
                inputField = new DatePicker();
                (inputField as DatePicker).Date = DateTime.Now;
                Value = (inputField as DatePicker).Date.ToString("dd/MM/yyyy");
                (inputField as DatePicker).DateSelected += (sender, args) =>
                {
                    Value = (sender as DatePicker).Date.ToString("dd/MM/yyyy");
                };
                inputField.Unfocused += (sender, args) =>
                {
                    Value = (sender as DatePicker).Date.ToString("dd/MM/yyyy");
                };
                (inputField as DatePicker).DateSelected += (sender, args) =>
                {
                    if (ValidationFunction != null)
                    {
                        var validationResult = this.ValidationFunction(InputName, Value);
                        errorLabel.Text = string.IsNullOrEmpty(validationResult) ? string.Empty : validationResult;
                        errorLabel.IsVisible = !string.IsNullOrEmpty(validationResult);
                    }
                };
                break;
            case InputType.Email:
            default:
                inputField = new Entry();
                inputField.Unfocused += (sender, args) =>
                {
                    Value = (sender as Entry).Text;
                };
                inputField.SetBinding(Entry.PlaceholderProperty, new Binding("Placeholder"));
                AddValidationHandler(inputField, errorLabel);
                break;
        }
        inputField.BindingContext = this;
        inputField.WidthRequest = 250;
        FormInputContainter.Children.Add(inputField);
        FormInputContainter.Children.Add(ErrorLabel = errorLabel);
        ChangeVisualState();
    }

    private void AddValidationHandler(View source, Label target)
    {
        source.Unfocused += (sender, args) =>
        {
            if (ValidationFunction != null)
            {
                var validationResult = this.ValidationFunction(InputName, Value);
                target.Text = string.IsNullOrEmpty(validationResult) ? string.Empty : validationResult;
                target.IsVisible = !string.IsNullOrEmpty(validationResult);
            }
        };
    }

    public enum InputType
    {
        TextArea,
        Text,
        Email,
        Date,
        Password
    }
}