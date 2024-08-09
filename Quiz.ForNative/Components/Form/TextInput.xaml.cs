
namespace Quiz.ForNative.Components.Form;

public partial class TextInput : ContentView, IFormInput<string>
{
    public readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(LabelContent), typeof(string), typeof(TextInput), default(string));

    public readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(PlaceholderContent), typeof(string), typeof(TextInput), default(string));

    public string LabelContent
    {
        get => GetValue(LabelProperty) as string;
        set => SetValue(LabelProperty, value);
    }

    public string PlaceholderContent
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    public InputValidationFunction ValidationFunction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string InputName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public TextInput()
    {
        InitializeComponent();
    }
}