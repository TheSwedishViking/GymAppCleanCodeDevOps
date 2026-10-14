using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using Moq;
using Xunit;

namespace GymSwipe.UserTests
{
    public class CardSwipeFacadeTests
    {
        [Fact]
        public async Task TestCardSwipeFacadeInitialization()
        {
            // Arrange
            var mockExerciseService = new Mock<IExerciseService>();
            var mockPlaylistService = new Mock<IGymPlaylistService>();
            var mockTrainingAreaService = new Mock<ITraningAreaService>();
            var mockTrainingAreaClient = new Mock<ITrainingAreasClient>();

            var exercises = new List<ExerciseDTO>
            {
                new()
                {
                    Id = 1,
                    Name = "Test Exercise",
                    VideoSourceLink = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Test Exercise 2",
                    VideoSourceLink = string.Empty
                },
                 new()
                {
                    Id = 3,
                    Name = "Test Exercise 3",
                    VideoSourceLink = string.Empty
                }
            };

            mockExerciseService
                .Setup(service => service.GetAllExercisesAsync())
                .ReturnsAsync(exercises);

            mockTrainingAreaService
                .Setup(service => service.GetExerciseTargetsAsync())
                .ReturnsAsync(new List<TargetAreaDTO>());

            var facade = new CardSwipeFacade(
                mockExerciseService.Object,
                mockPlaylistService.Object,
                mockTrainingAreaService.Object,
                mockTrainingAreaClient.Object,
                new LoggedInUser());

            // Act
            await facade.LoadExercisesAsync();

            var approvedCard = await facade.DrawNewExerciseCard();
            var approvedExercise = await facade.ApproveCard(approvedCard);
            facade.AddedExercises.Add(approvedExercise);
            await facade.DrawNewExerciseCard();
            await facade.DiscardCard();

            // Assert
            Assert.Equal(facade.AddedExercises[0], approvedCard);
            Assert.True(facade.HasDrawnAllCards());
        }
    }
}
