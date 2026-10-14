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
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Request_TargetOverAPI_AllValid(int id )
        {
            var area = await _fixture.TrainingClient.GetTargetAreaDTOById(id);

            Assert.NotNull(area);
        }
        [Theory]
        [InlineData(0, HttpStatusCode.BadRequest)]
        [InlineData(251, HttpStatusCode.NotFound)]
        [InlineData(-5555, HttpStatusCode.BadRequest)]
        public async Task Get_TargetAreaDTO_InvalidId_OrMissing_ThrowsExpectedStatusCodes(int id, HttpStatusCode code)
        {
            var area = await Assert.ThrowsAsync<HttpRequestException>(() => _fixture.TrainingClient.GetTargetAreaDTOById(id));

            Assert.Equal(code, area.StatusCode);
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
