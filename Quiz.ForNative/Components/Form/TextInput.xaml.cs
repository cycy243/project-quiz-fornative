
namespace Quiz.ForNative.Components.Form;

public partial class TextInput : ContentView, IFormInput<string>
{
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(TextInput),
        default(string));
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(PlaceholderContent),
        typeof(string),
        typeof(TextInput),
        default(string));
    public static readonly BindableProperty ValidationRulesProperty = BindableProperty.Create(
        nameof(ValidationRules),
        typeof(string),
        typeof(TextInput),
        default(string));
    public static readonly BindableProperty ValidationFunctionProperty = BindableProperty.Create(
        nameof(ValidationFunction),
        typeof(InputValidationFunction),
        typeof(TextInput));
    public static readonly BindableProperty InputNameProperty = BindableProperty.Create(
        nameof(InputName),
        typeof(string),
        typeof(TextInput),
        default(string));

    public InputValidationFunction ValidationFunction
    {
        get => GetValue(ValidationFunctionProperty) as InputValidationFunction;
        set => SetValue(ValidationFunctionProperty, value);
    }

    public string LabelContent
    {
        get => GetValue(LabelProperty) as string;
        set => SetValue(LabelProperty, value);
    }
    public string PlaceholderContent
    {
        get => GetValue(PlaceholderProperty) as string;
        set => SetValue(PlaceholderProperty, value);
    }
    public string ValidationRules
    {
        get => GetValue(ValidationRulesProperty) as string;
        set => SetValue(ValidationRulesProperty, value);
    }

    public string InputName
    {
        get => GetValue(InputNameProperty) as string;
        set => SetValue(InputNameProperty, value);
    }

    public bool Validate()
    {
        string validationResult = ValidationFunction(InputName, Input.Value);
        bool isEmpty = string.IsNullOrEmpty(validationResult);
        if (!isEmpty)
        {
            Input.ErrorTxt = validationResult;
        }
        return isEmpty;
    }

    public TextInput()
    {
        InitializeComponent();
    }
}