using CommunityToolkit.Maui.Alerts;

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

    public static readonly BindableProperty PlaceholderContentProperty = BindableProperty.Create(
        nameof(PlaceholderContent),
        typeof(string),
        typeof(PictureInput),
        "Select a file...");

    public string PlaceholderContent
    {
        get => GetValue(PlaceholderContentProperty) as string;
        set => SetValue(PlaceholderContentProperty, value);
    }

    public FileResult? SelectedFile { get; private set; }
    public InputValidationFunction ValidationFunction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string InputName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public PictureInput()
    {
        InitializeComponent();
    }

    private async void UploadBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            PickOptions options = new()
            {
                PickerTitle = "Please select a comic file",
            };
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    SelectedFile = result;
                    PlaceholderContent = result.FileName;
                }
            }
        }
        catch (Exception ex)
        {
            await Snackbar.Make("An error occured").Show();
        }
    }
}
