using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
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

            var exercises = new List<ExerciseDTO> //4
            {
                new()
                {
                    Id = 1,
                    Name = "Card One",
                    VideoSourceLink = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Card Two",
                    VideoSourceLink = string.Empty
                },
                 new()
                {
                    Id = 3,
                    Name = "Card Three",
                    VideoSourceLink = string.Empty
                },
                 new()
                {
                    Id = 4,
                    Name = "Card Four",
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
                new LoggedInUser());



            // Act
            await facade.LoadExercisesAsync();
            _output.WriteLine($"Available: {facade.AvailableExercises.Count} cards.");

            var approvedCard = await facade.DrawNewExerciseCard();
            var cardAfterApproval = await facade.ApproveCard(approvedCard);
            _output.WriteLine($"Approved: {approvedCard.Name}");

            _output.WriteLine($"Discarded: {cardAfterApproval.Name}");
            var cardAfterFirstDiscard = await facade.DiscardCard();

            _output.WriteLine($"Discarded: {cardAfterFirstDiscard.Name}");
            var finalCard = await facade.DiscardCard();

            
            Assert.True(facade.HasDrawnAllCards());
            _output.WriteLine($"Final card awaiting decision: {finalCard.Name}");

            var nextCard = await facade.ApproveCard(finalCard);
            _output.WriteLine($"Approved final card: {finalCard.Name}");

            // Assert
            Assert.Equal(2, facade.AddedExercises.Count);
            Assert.Same(approvedCard, facade.AddedExercises[0]);
            Assert.Same(finalCard, facade.AddedExercises[1]);
            Assert.Null(nextCard);
            Assert.True(facade.HasDrawnAllCards());
        }
    }
}
//facade.GetApproptiateImageForExercise(approvedCard.Id); //work in progress, not tested yet
