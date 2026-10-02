namespace GymSwipe
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.UserPage), typeof(Pages.UserPage));
            Routing.RegisterRoute(nameof(Pages.UserRegistrationPage), typeof(Pages.UserRegistrationPage));
            Routing.RegisterRoute(nameof(Pages.RandomExercisePage), typeof(Pages.RandomExercisePage));
            Routing.RegisterRoute(nameof(Pages.AdminRegisterNewExercise), typeof(Pages.AdminRegisterNewExercise));
            Routing.RegisterRoute(nameof(Pages.AdminRegisterNewTrainingArea), typeof(Pages.AdminRegisterNewTrainingArea));
            Routing.RegisterRoute(nameof(Pages.AdminViewExercise), typeof(Pages.AdminViewExercise));
            Routing.RegisterRoute(nameof(Pages.AdminViewTraningArea), typeof(Pages.AdminViewTraningArea));


        }
    }
}
