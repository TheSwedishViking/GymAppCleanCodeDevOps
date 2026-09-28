namespace GymSwipe
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.UserPage), typeof(Pages.UserPage));
            Routing.RegisterRoute(nameof(Pages.UserRegistrationPage), typeof(Pages.UserRegistrationPage));
            Routing.RegisterRoute(nameof(Pages.CardSwipe), typeof(Pages.CardSwipe));

        }
    }
}
