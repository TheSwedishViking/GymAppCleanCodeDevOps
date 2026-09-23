namespace GymSwipe
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.UserPage), typeof(Pages.UserPage));
        }
    }
}
