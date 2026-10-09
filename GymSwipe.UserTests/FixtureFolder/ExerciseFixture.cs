using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            var connString = config["Connections:LocalConnection"];

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

            services.AddScoped<IExerciseRepository, ExerciseRepo>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();
            services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            services.AddScoped<ITraningAreaService, TrainingAreaService>();

            ServiceProvider = services.BuildServiceProvider();
        }
        public async Task DisposeAsync()
        {
            if (ServiceProvider != null)
            {
                await ServiceProvider.DisposeAsync();
            }
        }

        public async Task InitializeAsync()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<IDatabaseInitalizer>();
                await init.InitalizeAsync();
            }
        }
    }
}
