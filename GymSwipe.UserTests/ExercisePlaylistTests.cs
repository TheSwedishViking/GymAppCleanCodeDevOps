using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class ExercisePlaylistTests : IClassFixture<ExerciseFixture>
    {
        private readonly ExerciseFixture _fixture;
        private readonly IServiceScope _scope;

        //private readonly IExerciseFacade _exerciseFacade;
        //private readonly IExerciseService _exerciseSut;

        //private readonly IWorkoutFacade _workoutFacade;

        private readonly ICardSwipeFacade _cardSwipeFacade;
        private readonly IGymPlaylistService _gymPlaylistService;

        public ExercisePlaylistTests(ExerciseFixture fixture)
        {
            _fixture = fixture;
            _scope = fixture.ServiceProvider.CreateScope();

            _cardSwipeFacade = _scope.ServiceProvider.GetRequiredService<ICardSwipeFacade>();
            _gymPlaylistService = _scope.ServiceProvider.GetRequiredService<IGymPlaylistService>();
        }
        public void Dispose() => _scope.Dispose();


        [Fact]
        public async Task CreatePlaylist_AddExercises_For_A_User_EnsureCreated()
        {
            int chestId = 1;
            await _cardSwipeFacade.InitalizeAsync();
            await _cardSwipeFacade.LoadExercisesById(chestId);

            GymUser testTestsson = new GymUser
            {
                Id = 1,
                Firstname = "Test",
                Surname = "Testsson",
                Email = "Test@Test.com",
                Gender = true,
                HeightCm = 190,
                WeightKg = 90,

            };
            var loadedExercises = await _cardSwipeFacade.GetLoadedExercises();

            Assert.NotEmpty(loadedExercises);

           for(int i = 0; i < loadedExercises.Count; i++)
            {
                if(i%2 == 0)
                {
                    await _cardSwipeFacade.ApproveCard(loadedExercises[i]);
                }
                else
                {
                    await _cardSwipeFacade.DiscardCard();
                }
            }

            await _cardSwipeFacade.SavePlayList(testTestsson);

            var playlist = await _gymPlaylistService.GetPlaylist(1);

            Assert.NotNull(playlist);

        }
     

    }
}
