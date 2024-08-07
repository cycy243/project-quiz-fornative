namespace Quiz.ForNative.Components.Form;

public partial class MailInput : ContentView, IFormInput<string>
{
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(LabelContent),
        typeof(string),
        typeof(MailInput),
        default(string));
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(PlaceholderContent),
        typeof(string),
        typeof(MailInput),
        default(string));
    public static readonly BindableProperty ValidationRulesProperty = BindableProperty.Create(
        nameof(ValidationRules),
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
    public string ValidationRules
    {
        get => GetValue(ValidationRulesProperty) as string;
        set => SetValue(ValidationRulesProperty, value);
    }

    private bool HasError = false;
    private string Error = string.Empty;

    public MailInput()
    {
        InitializeComponent();
    }

    private void ValidateInput(object sender, TextChangedEventArgs e)
    {
        HasError = false;
        Error = string.Empty;
        var input = this.Input.Text;
        foreach (var rule in ValidationRules.Split("|"))
        {
            var items = rule.Split(":");
            Error = ValidateFor(input, items[0], items.Length == 1 ? "" : items[1]);
            if (Error != string.Empty)
            {
                HasError = true;
                break;
            }
        }
    }

    private string ValidateFor(string input, string rule, string constraint)
    {
        switch (rule)
        {
            case "required":
                return input.Length == 0 || string.IsNullOrWhiteSpace(input) ? $"The mail is required" : string.Empty;
            case "max":
                return input.Length < int.Parse(constraint) ? string.Empty : $"The mail shouldn't have more than {constraint} characters";
            default:
                return string.Empty;
        }
    }
}
