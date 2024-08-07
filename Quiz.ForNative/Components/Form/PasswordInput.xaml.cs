namespace Quiz.ForNative.Components.Form;

public partial class PasswordInput : ContentView
{
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(PasswordInput),
        default(string));
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(PlaceholderContent),
        typeof(string),
        typeof(MailInput),
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

    public PasswordInput()
    {
        InitializeComponent();
    }
}
