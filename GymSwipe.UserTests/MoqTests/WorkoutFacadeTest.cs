using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using Moq;
using Xunit;

namespace GymSwipe.UserTests.MoqTests
{
    public class WorkoutFacadeTest
    {
        [Fact]
        public async Task TestActiveWorkoutFacadeIntitialization()
        {
            var exerciseService = new Mock<IExerciseService>();
            var userService = new Mock<IUserService>();
            var calorieCountingService = new Mock<ICalorieCountingService>();
            var timeSetter = new Mock<ITimeSetter>();
            var playlistControls = new Mock<IPlaylistControls>();
            var playlistService = new Mock<IGymPlaylistService>();
            var trainingAreaService = new Mock<ITraningAreaService>();

            var loggedInUser = new LoggedInUser
            {
                CurrentUser = new() { WeightKg = 100 }
            };

            var facade = new WorkoutFacade(
                exerciseService.Object,
                userService.Object,
                calorieCountingService.Object,
                timeSetter.Object,
                playlistControls.Object,
                playlistService.Object,
                trainingAreaService.Object,
                loggedInUser,
                new WorkoutSession());

            var exercises = new List<PlaylistExerciseDTO>
            {
                new() { ExerciseId = 1, Name = "Bench Press", VideoSourceLink = string.Empty },
                new() { ExerciseId = 2, Name = "Pullups", VideoSourceLink = string.Empty },
                new() { ExerciseId = 3, Name = "Squats", VideoSourceLink = string.Empty },
            };

            var playlist = new GymPlaylistDTO
            {
                Id = 1,
                Excercise = exercises
            };

            facade._playlist = playlist;


            Assert.NotNull(facade.stopwatch);
            Assert.Same(playlist, facade._playlist);
            Assert.Equal(3, facade._playlist.Excercise.Count);
        }
    }
}