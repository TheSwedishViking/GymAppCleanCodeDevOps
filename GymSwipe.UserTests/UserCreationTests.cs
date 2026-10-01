using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.UserTests.FixtureFolder;
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
                Firstname = "Peter",
                Surname = "Stormare",
                Gender = true,
                HeightCm = 200,
                WeightKg = 90
            };

            var response = await _client.PostAsJsonAsync("api/User", request);

            response.EnsureSuccessStatusCode();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreatedUser_ValidatedNames_ReturnsExpected()
        {
            var request = new RequestCreateGymUserDTO
            {
                Firstname = "Peter",
                Surname = "Stormare",
                Gender = true,
                HeightCm = 200,
                WeightKg = 90
            };

            var service = new UserInputValidatorService();
            request.Firstname = service.UserNameValidator(request.Firstname);
            request.Surname = service.UserNameValidator(request.Surname);

            var response = await _client.PostAsJsonAsync("api/User", request);

            response.EnsureSuccessStatusCode();
            var returnedUser = await response.Content.ReadFromJsonAsync<GymUserDTO>();


            Assert.Equal("Peter", returnedUser.Firstname);
        }


    }
}
