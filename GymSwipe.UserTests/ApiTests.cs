using Xunit;

namespace GymSwipe.UserTests
{
    public class ApiTests : IClassFixture<UserApiFixture>
    {
        private readonly HttpClient _client;

        public ApiTests(UserApiFixture factory)
        {
            _client = factory.GetClient();
        }

        [Fact]
        public async Task ApiIsHealthy()
        {
            // Arrange
            var requestUri = "/api/User/1";

            // Act
            var response = await _client.GetAsync(requestUri);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.True(response.Content.Headers.ContentLength > 0);
        }




    }
}
