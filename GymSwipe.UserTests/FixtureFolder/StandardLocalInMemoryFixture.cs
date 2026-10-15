using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces;
using GymSwipe.ApplicationLayer.Interfaces.Controls;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using GymSwipe.ApplicationLayer.Services.ClientServices;
using GymSwipe.ApplicationLayer.Services.Controls;
using GymSwipe.ApplicationLayer.Services.Mappers;
using GymSwipe.ApplicationLayer.Services.Services;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.ApplicationLayer.Services.TimeDateFormat;
using GymSwipe.ApplicationLayer.Services.Validators;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GymSwipe.UserTests.FixtureFolder
{
    public class StandardLocalInMemoryFixture : IAsyncLifetime
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public StandardLocalInMemoryFixture()
        {
            var services = new ServiceCollection();

                services.AddDbContext<GymAppDbContext>(db => db.UseInMemoryDatabase("GymAppTestDb"));
            //*******CLIENT SERVICES*******
            services.AddHttpClient<ITrainingAreasClient, TrainingAreaClient>(c =>
                c.BaseAddress = new Uri("http://localhost/"));
            //*******SERVICES*******
            services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITraningAreaService, TrainingAreaService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IGymPlaylistService, GymPlaylistService>();
            services.AddScoped<IDateHandler, DateHandler>();
            services.AddScoped<ITimeSetter, TimeSetter>();
            services.AddScoped<IAdminCreatorValidator, AdminCreatorValidator>();
            services.AddScoped<IRecordsValidator, RecordsValidator>();
            services.AddScoped<IPlaylistControls, PlaylistControls>();
            services.AddScoped<IExerciseRecordsService, ExerciseRecordsService>();
            services.AddScoped<ICalorieCountingService, CalorieCountingService>();

            //********MAPPING SERVICES************
            services.AddScoped<IMapDomainService, MapDomainService>();
            services.AddScoped<IMapDTOService, MapDTOService>();

            //*******REPOSITORIES*******
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepo>();
            services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            services.AddScoped<IGymPlaylistRepository, GymPlaylistRepo>();
            services.AddScoped<IExerciseRecordsRepository, ExerciseRecordsRepository>();

            //*******FACADES*******
            services.AddScoped<IExerciseFacade, ExerciseFacade>();
            services.AddScoped<IAdminAddExerciseFacade, AdminAddExerciseFacade>();
            services.AddScoped<IAdminAddTrainingAreaFacade, AdminAddTraningAreaFacade>();
            services.AddScoped<ICardSwipeFacade, CardSwipeFacade>();
            services.AddScoped<IWorkoutFacade, WorkoutFacade>();
            services.AddScoped<IExerciseRecordsFacade, ExerciseRecordsFacade>();

            //****SINGLETON SESSION*****
            services.AddScoped<LoggedInUser>();

            ServiceProvider = services.BuildServiceProvider();
        }
        public async Task DisposeAsync()
        {
            if (ServiceProvider != null)
            {
                await ServiceProvider.DisposeAsync();
            }
        }
        public  async Task InitializeAsync()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<IDatabaseInitalizer>();
                await init.InitalizeAsync();
            }
        }
    }
}
