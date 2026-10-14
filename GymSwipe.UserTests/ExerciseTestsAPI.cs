using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.UserTests.FixtureFolder;
using System.Net;
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

        [Theory]
        [InlineData(1, true)]
        [InlineData(21, false)]
        [InlineData(-15, false)]
        public async Task Request_TargetOverAPI_ExpectedResult(int id, bool exists)
        {
            var area = await _fixture.TrainingClient.GetTargetAreaDTOById(id);

            if (exists)
            {
                Assert.NotNull(area);
            }
        }

        [Theory]
        [InlineData("api/TrainingArea/get-area-by-id/1", HttpStatusCode.OK)]
        [InlineData("api/TrainingArea/get-area-by-id/999", HttpStatusCode.NotFound)]
        [InlineData("api/TrainingArea/get-area-by-id/-50", HttpStatusCode.BadRequest)]
        public async Task RequestMultipleDifferentAreas_ReturnsExpectedNetworkCode(string url, HttpStatusCode code)
        {
            var response = await _fixture.HttpClient.GetAsync(url);

            Assert.Equal(response.StatusCode, code);
        }
    }
}
