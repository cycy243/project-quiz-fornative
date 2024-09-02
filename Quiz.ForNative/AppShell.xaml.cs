using Quiz.ForNative.Views;
using Quiz.ForNative.Views.Auth;

namespace Quiz.ForNative
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
        }
    }
}
