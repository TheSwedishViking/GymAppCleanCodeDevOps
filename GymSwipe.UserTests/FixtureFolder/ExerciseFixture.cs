using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.UserTests.FixtureFolder
{
    public class ExerciseFixture : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public ExerciseFixture()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ExerciseFixture>().Build();

            var services = new ServiceCollection();

            services.AddDbContext<GymAppDbContext>(d => d.UseSqlServer(config["Connections:LocalConnection"]));

            services.AddScoped<IExerciseRepository, ExerciseRepo>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            services.AddScoped<ITraningAreaService, TrainingAreaService>();
            ServiceProvider = services.BuildServiceProvider();
        }
        public void Dispose() => ServiceProvider.Dispose();
        
    }
}
