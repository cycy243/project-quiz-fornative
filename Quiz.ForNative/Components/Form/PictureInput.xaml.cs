namespace Quiz.ForNative.Components.Form;

public partial class PictureInput : ContentView, IFormInput<string>
{
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(PictureInput),
        default(string));

    public string LabelContent
    {
        get => GetValue(LabelProperty) as string;
        set => SetValue(LabelProperty, value);
    }
    public string PlaceholderContent { get => ""; set{ } }

    public PictureInput()
    {
        InitializeComponent();
    }
}
