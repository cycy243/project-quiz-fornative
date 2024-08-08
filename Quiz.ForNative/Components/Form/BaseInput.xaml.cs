using System.Windows.Input;

namespace Quiz.ForNative.Components.Form;

public partial class BaseInput : ContentView
{
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(BaseInput),
        default(string));
    public static readonly BindableProperty LabelClickedCommandProperty = BindableProperty.Create(
        nameof(LabelClickedCommandProperty),
        typeof(string),
        typeof(BaseInput),
        default(string));

    ContentPresenter InputEntry;
    Label InputLabel;

    public ICommand LabelClickedCommand => new Command(() =>
    {

    });

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public BaseInput()
    {
        InitializeComponent();
        //LabelClickedCommand = new Command(() =>
        //{
        //    (this.baseInput.FindByName("input_container") as Frame).Focus();
        //});
    }

    // Event handler for when the Label is tapped
    private void OnLabelTapped(object sender, EventArgs e)
    {
        // Get the first child in the ContentPresenter that is focusable
        if (FindVisualElement<ContentPresenter>(this).Content is View view)
        {
            view.Focus(); // Set focus to the child view
        }
    }

    // Utility method to find an element of a specific type within the visual tree
    private T FindVisualElement<T>(Element element) where T : Element
    {
        if (element is T typedElement)
        {
            return typedElement;
        }

        if (element is IVisualTreeElement visualTreeElement)
        {
            foreach (var child in visualTreeElement.GetVisualChildren())
            {
                var result = FindVisualElement<T>(child as Element);
                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }

}
