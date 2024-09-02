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
    public static readonly BindableProperty ValidationFunctionProperty = BindableProperty.Create(
        nameof(ValidationFunction),
        typeof(InputValidationFunction),
        typeof(PictureInput));
    public static readonly BindableProperty InputNameProperty = BindableProperty.Create(
        nameof(InputName),
        typeof(string),
        typeof(PictureInput),
        default(string));

    public string PlaceholderContent
    {
        get => GetValue(PlaceholderContentProperty) as string;
        set => SetValue(PlaceholderContentProperty, value);
    }

    public FileResult? SelectedFile { get; private set; }
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
        string validationResult = ValidationFunction(InputName, PlaceholderContent);
        bool isEmpty = string.IsNullOrEmpty(validationResult);
        if (!isEmpty)
        {
            // ErrorTxt = validationResult;
        }
        return isEmpty;
    }

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
                PickerTitle = "Please select an avatar",
            };
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    SelectedFile = result;
                    PlaceholderContent = result.FileName;
                    ValidationFunction(nameof(PictureInput), result.FullPath);
                }
            }
        }
        catch (Exception)
        {
            await Snackbar.Make("An error occured").Show();
        }
    }
}
