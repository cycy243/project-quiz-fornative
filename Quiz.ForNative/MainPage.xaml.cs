using Quiz.ForNative.Views.Auth;

namespace Quiz.ForNative
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginView));
        }

        private async void OnRegister(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegisterView));
        }
    }

}
