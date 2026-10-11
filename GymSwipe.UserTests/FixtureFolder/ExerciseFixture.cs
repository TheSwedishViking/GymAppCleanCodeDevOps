using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests.FixtureFolder
{
    public class ExerciseFixture : IAsyncLifetime
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public ExerciseFixture()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ExerciseFixture>(optional: true).Build();

            var services = new ServiceCollection();

            var connString = "";

            //Use in memory
            if (string.IsNullOrEmpty(connString))
            {
                services.AddDbContext<GymAppDbContext>(db => db.UseInMemoryDatabase("GymAppTestDb"));
            }
            //Local machine; use server
            else
            {
                services.AddDbContext<GymAppDbContext>(d => d.UseSqlServer(connString));

            }

            //*******SERVICES*******
            services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITraningAreaService, TrainingAreaService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IGymPlaylistService, GymPlaylistService>();
            services.AddScoped<IDateHandler, DateHandler>();
            services.AddScoped<IExerciseRecordsService, ExerciseRecordsService>();

            //*******REPOSITORIES*******
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepo>();
            services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            services.AddScoped<IGymPlaylistRepository, GymPlaylistRepo>();
            services.AddScoped<IExerciseRecordsRepository, ExerciseRecordsRepository>();

            //*******FACADES*******
            services.AddScoped<IUserFacade, UserActionsFacade>();
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
            if(ServiceProvider != null)
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
