using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.UserTests.FixtureFolder;
using System.Net.Http.Json;
using Xunit;

namespace GymSwipe.UserTests
{
    [Collection("api")]
    public class ExerciseTestsAPI 
    {
        private readonly APIExerciseFixture _fixture;
        public ExerciseTestsAPI(APIExerciseFixture aPIExerciseFixture)
        {
            _fixture = aPIExerciseFixture;
        }
        [Fact]
        public async Task RequestAllExercies_ValidateThatCollectionIsNotEmpty_ReturnsExpected_LoadsOfExercises()
        {
            var areas = await _fixture.TrainingClient.GetTargetAreaDTOsAsync();
            Assert.NotEmpty(areas);
        }
        [Fact]
        public async Task Request_ChestExercieses_ReturnsExpected_Collection()
        {
            var expected = "Chest";
            var response = await _client.GetAsync("api/Exercise/get-chest-exercises");
            response.EnsureSuccessStatusCode();

            var dtos = await response.Content.ReadFromJsonAsync<List<ExerciseDTO>>();

            Assert.NotEmpty(dtos);
            Assert.All(dtos, c => Assert.Contains(c.TargetAreaNames, n => n.Name.Contains(expected, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
