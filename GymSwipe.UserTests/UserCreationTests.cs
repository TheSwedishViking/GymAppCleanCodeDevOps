using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace GymSwipe.UserTests
{
    public class UserCreationTests : IClassFixture<UserApiFixture>
    {

        private readonly HttpClient _client;

        public UserCreationTests(UserApiFixture fixture)
        {
            _client = fixture.CreateClient();
        }


        [Fact]
        public async Task CreateadUser_IsSavedToDb_ReturnsExpected()
        {

            var request = new RequestCreateGymUserDTO
            {
                Firstname = "Usain",
                Surname = "Bolt",
                HeightCm = 200,
                WeightKg = 90
            };

            var response = await _client.PostAsJsonAsync("api/User", request);

            response.EnsureSuccessStatusCode();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
