using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Services.Services;
using GymSwipe.Domain.Models;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class PlaylistTests : IClassFixture<StandardLocalInMemoryFixture>
    {
        private readonly StandardLocalInMemoryFixture _fixture;
        private readonly IServiceScope _scope;
        private readonly IExerciseService _exerciseService;
        private readonly IGymPlaylistService _gymplaylistService;
        public PlaylistTests(StandardLocalInMemoryFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateScope();
            _exerciseService = _scope.ServiceProvider.GetRequiredService<IExerciseService>();
            _gymplaylistService = _scope.ServiceProvider.GetRequiredService<IGymPlaylistService>();
        }

        [Fact]
        public async Task SaveExercisesToUser_CreatesAPlaylist_ReturnCreated()
        {
            var exs = await _exerciseService.GetRandomExercisesAsync();

            var user = new GymUser
            {
                Id = 66,
                Firstname = "Oscar",
                Surname = "Stenström",
                Email = "s@s.com",
                Gender = true,
                HeightCm = 187,
                WeightKg = 81.5,
            };
            await _gymplaylistService.SavePlaylistToUser(exs, user);

            var foundUserPlaylist = await _gymplaylistService.GetPlaylistsByUserId(user.Id);

            Assert.NotEmpty(foundUserPlaylist);
        }

    }
}
