using Microsoft.Maui.Handlers;
using System.Windows.Input;
using Microsoft.Maui.Platform;

namespace Quiz.ForNative.Components.Form;

public partial class BaseInput : Microsoft.Maui.Controls.ContentView
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

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public BaseInput()
    {
        InitializeComponent();
    }

    // Event handler for when the Label is tapped
    private void OnLabelTapped(object sender, EventArgs e)
    {
        // Get the first child in the ContentPresenter that is focusable
        var containerContent = FindVisualElement<Microsoft.Maui.Controls.ContentPresenter>(this);
        if (containerContent?.Content is View view)
        {
            view.Focus(); // Set focus to the child view
        }
#if ANDROID
        if (containerContent?.Content is Microsoft.Maui.Controls.DatePicker picker)
        {
            (picker.Handler as ITimePickerHandler)?.PlatformView.PerformClick();
        }
#endif
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
                var result = FindVisualElement<T>((child as Element)!);
                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }

}
