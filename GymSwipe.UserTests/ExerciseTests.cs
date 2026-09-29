using GymSwipe.ApplicationLayer.Services;
using Xunit;

namespace GymSwipe.UserTests
{
    public class ExerciseTests
    {

        [Fact]
        public async Task TryAndGetStaticExcerises_ReturnsExpectedTestaData_FormatDTO_ToConvertToFrontendModels()
        {
            //Arrange
            var sut = new ExerciseService();

            //Act
            var exs = await sut.GetAllExercisesAsync();

            //Assert
            Assert.NotEmpty(exs);

        }


    }
}
