using GymSwipe.API;
using GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces;
using GymSwipe.ApplicationLayer.Services.ClientServices;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests.FixtureFolder
{
    public class APIExerciseFixture : IAsyncLifetime
    {
        //Get the webaddress from API, localhost with port number
        private WebApplicationFactory<Program> _factory = default!;
        public HttpClient HttpClient { get; private set; } = default!;
        public ITrainingAreasClient TrainingClient { get; private set; } = default!;

        public async Task DisposeAsync()
        {
            HttpClient.Dispose();
            await _factory.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove possible dbs
                    services.RemoveAll<DbContextOptions<GymAppDbContext>>();
                    services.RemoveAll<IDbContextOptionsConfiguration<GymAppDbContext>>();
                    services.RemoveAll<GymAppDbContext>();

                    // In memory like other ExerciseFixture
                    services.AddDbContext<GymAppDbContext>(db =>
                        db.UseInMemoryDatabase("GymAppApiTestDb"));
                });
            });

            //Load sample data in scope
            using (var scope = _factory.Services.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<IDatabaseInitalizer>();
                await init.InitalizeAsync();
            }

            HttpClient = _factory.CreateClient();

            TrainingClient = new TrainingAreaClient(HttpClient);
        }
    }
}
