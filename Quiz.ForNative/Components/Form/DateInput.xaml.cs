namespace Quiz.ForNative.Components.Form;

public partial class DateInput : ContentView, IFormInput<DateOnly>
{
    public readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(LabelContent), typeof(string), typeof(TextInput), default(string));

    public readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(PlaceholderContent), typeof(DateOnly), typeof(TextInput), default(DateOnly));
    public static readonly BindableProperty ValidationFunctionProperty = BindableProperty.Create(
        nameof(ValidationFunction),
        typeof(InputValidationFunction),
        typeof(DateInput));
    public static readonly BindableProperty InputNameProperty = BindableProperty.Create(
        nameof(InputName),
        typeof(string),
        typeof(DateInput),
        default(string));

    public string LabelContent
    {
        get => GetValue(LabelProperty) as string;
        set => SetValue(LabelProperty, value);
    }

    public DateOnly PlaceholderContent
    {
        get => (DateOnly)GetValue(PlaceholderProperty);
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

    public DateInput()
    {
        InitializeComponent();
    }
}
