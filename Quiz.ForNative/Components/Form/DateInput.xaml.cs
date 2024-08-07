namespace Quiz.ForNative.Components.Form;

public partial class DateInput : ContentView, IFormInput<DateOnly>
{
    public readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(LabelContent), typeof(string), typeof(TextInput), default(string));

    public readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(PlaceholderContent), typeof(DateOnly), typeof(TextInput), default(DateOnly));

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

    public DateInput()
    {
        InitializeComponent();
    }
}
