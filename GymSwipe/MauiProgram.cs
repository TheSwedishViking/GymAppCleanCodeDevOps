using GymSwipe.Application.Facades;
using GymSwipe.Application.Interfaces;
using GymSwipe.Application.Services;
using GymSwipe.ViewModels;
using Microsoft.Extensions.Logging;

namespace GymSwipe
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


#if DEBUG
    		builder.Logging.AddDebug();

            //*******SERVICES*******
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IPlaylistService, PlaylistService>();

            //*******REPOSITORIES*******
            builder.Services.AddScoped<IUserRepository>();
            builder.Services.AddScoped<IExerciseRepository>();
            builder.Services.AddScoped<IPlaylistRepository>();

            //*******FACADES*******
            builder.Services.AddScoped<IUserFacade, UserActionsFacade>();
            builder.Services.AddScoped<IExerciseFacade>();
            builder.Services.AddScoped<IPlaylistFacade>();  

            //*******VIEW MODELS*******
            builder.Services.AddTransient<UserPageAccountViewModel>();
            builder.Services.AddTransient<UserRegisterViewModel>();

#endif

            return builder.Build();
        }
    }
}
