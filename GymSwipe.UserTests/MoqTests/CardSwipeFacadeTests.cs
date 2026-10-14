using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GymSwipe.UserTests
{
    public class CardSwipeFacadeTests
    {
        private readonly ITestOutputHelper _output;
        public CardSwipeFacadeTests(ITestOutputHelper output)
        {
            _output = output;
        }



        [Fact]
        public async Task TestCardSwipeFacadeInitialization()
        {
            // Arrange
            var mockExerciseService = new Mock<IExerciseService>();
            var mockPlaylistService = new Mock<IGymPlaylistService>();
            var mockTrainingAreaService = new Mock<ITraningAreaService>();
            var mockTrainingAreaClient = new Mock<ITrainingAreasClient>();

            var targetAreas = new List<TargetAreaDTO>
            {
                new TargetAreaDTO { Id = 1, Name = "Chest" },
                new TargetAreaDTO { Id = 2, Name = "Back" },
                new TargetAreaDTO { Id = 3, Name = "Legs" },
                new TargetAreaDTO { Id = 4, Name = "Arms" },
            };

            var exercises = new List<ExerciseDTO> //4
            {
                new()
                {
                    Id = 1,
                    Name = "Bench Press",
                    VideoSourceLink = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Pullups ",
                    VideoSourceLink = string.Empty
                },
                 new()
                {
                    Id = 3,
                    Name = "Squats",
                    VideoSourceLink = string.Empty
                },
                 new()
                {
                    Id = 4,
                    Name = "Hammer Curls",
                    VideoSourceLink = string.Empty
                 }


            };

            mockExerciseService
                .Setup(service => service.GetAllExercisesAsync())
                .ReturnsAsync(exercises);


            mockTrainingAreaService
                .Setup(service => service.GetExerciseTargetsAsync())
                .ReturnsAsync(targetAreas);

            //Looks trough TargetAreaList for specific id, returns targetAreaName    //cant input id directly due to exercise being random order
            mockTrainingAreaService.Setup(getImg => getImg.GetTargetAreaImage(It.IsAny<int>())).ReturnsAsync(targetAreas[It.IsAny<int>()].Name);

            var facade = new CardSwipeFacade(
                mockExerciseService.Object,
                mockPlaylistService.Object,
                mockTrainingAreaService.Object,
                mockTrainingAreaClient.Object,
                new LoggedInUser());




            // Act
            await facade.LoadExercisesAsync();
            _output.WriteLine($"Available: {facade.AvailableExercises.Count} cards.");

            var approvedCard = await facade.DrawNewExerciseCard();
            await facade.ApproveCard(approvedCard);
            _output.WriteLine($"Approved: {approvedCard.Name}");

            await facade.DiscardCard();


            var finalCard = await facade.DiscardCard();  //returns card drawn to aprove


            Assert.True(facade.HasDrawnAllCards());
            _output.WriteLine($"Cards Left in deck: {facade.AvailableExercises.Count}");
            _output.WriteLine($"Final card in hand: " + finalCard.Name);

            var nextCard = await facade.ApproveCard(finalCard); //returns null, no more cards
            var image = await facade.GetApproptiateImageForExercise(finalCard.Id); //work in progress, not tested yet


            _output.WriteLine($"the final card will display an image of {image}");

            _output.WriteLine($"Approved final card: {finalCard.Name}");

            // Assert
            Assert.Equal(2, facade.AddedExercises.Count);
            Assert.Same(approvedCard, facade.AddedExercises[0]);
            Assert.Same(finalCard, facade.AddedExercises[1]);
            Assert.Contains("card", image);
            Assert.Null(nextCard);

        }
    }
}
