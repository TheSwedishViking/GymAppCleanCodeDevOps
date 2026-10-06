using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using GymSwipe.ViewModels;
using GymSwipe.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            //******DB CONTEXT*******
            builder.Configuration.AddUserSecrets<App>();
            var connstring = builder.Configuration["Connections:LocalConnection"];
            builder.Services.AddDbContext<GymAppDbContext>(opts =>
            {
                opts.UseSqlServer(connstring);
            });
            builder.Services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();

            //*******SERVICES*******
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITraningAreaService, TrainingAreaService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IGymPlaylistService, GymPlaylistService>();

            //*******REPOSITORIES*******
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepo>();
            builder.Services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            builder.Services.AddScoped<IGymPlaylistRepository, GymPlaylistRepo>();

            //*******FACADES*******
            builder.Services.AddScoped<IUserFacade, UserActionsFacade>();
            builder.Services.AddScoped<IExerciseFacade, ExerciseFacade>();
            builder.Services.AddScoped<IAdminAddExerciseFacade, AdminAddExerciseFacade>();
            builder.Services.AddScoped<IAdminAddTrainingAreaFacade, AdminAddTraningAreaFacade>();
            //builder.Services.AddScoped<IPlaylistFacade>();  

            //*******VIEW MODELS*******
            builder.Services.AddSingleton<LoggedInUser>();

            builder.Services.AddSingleton<UserPageAccountViewModel>();
            builder.Services.AddTransient<UserRegisterViewModel>();
            builder.Services.AddTransient<RandomExerciseViewModel>();
            builder.Services.AddTransient<AdminRegisterNewExerciseViewModel>();
            builder.Services.AddTransient<AdminRegisterNewTrainingAreaViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();

            //api address
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7277/")
            });
#endif
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<IDatabaseInitalizer>();

                Task.Run(() => init.InitalizeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
