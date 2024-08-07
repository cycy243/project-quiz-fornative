namespace Quiz.ForNative.Components.Form;

public partial class BaseInput : ContentView
{
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(BaseInput),
        default(string));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public BaseInput()
    {
        InitializeComponent();
        this.BindingContext = this;
    }
}
