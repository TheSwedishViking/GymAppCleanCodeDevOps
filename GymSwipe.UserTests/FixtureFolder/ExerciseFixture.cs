using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
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
            var config = new ConfigurationBuilder().AddUserSecrets<ExerciseFixture>().Build();

            var services = new ServiceCollection();

            services.AddDbContext<GymAppDbContext>(d => d.UseSqlServer(config["Connections:LocalConnection"]));

            services.AddScoped<IExerciseRepository, ExerciseRepo>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();

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
