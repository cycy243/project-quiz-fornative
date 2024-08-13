namespace Quiz.ForNative.Components.Form;

public partial class PasswordInput : ContentView, IFormInput<string>
{
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(PasswordInput),
        default(string));
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(PlaceholderContent),
        typeof(string),
        typeof(PasswordInput),
        default(string));
    public static readonly BindableProperty ValidationFunctionProperty = BindableProperty.Create(
        nameof(ValidationFunction),
        typeof(InputValidationFunction),
        typeof(PasswordInput));
    public static readonly BindableProperty InputNameProperty = BindableProperty.Create(
        nameof(InputName),
        typeof(string),
        typeof(PasswordInput),
        default(string));

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

    public PasswordInput()
    {
        InitializeComponent();
    }
}
