using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace GymSwipe.UserTests
{
    public class UserCreationTests : IClassFixture<UserApiFixture>
    {

        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public UserCreationTests(UserApiFixture fixture, ITestOutputHelper output)
        {
            _client = fixture.GetClient();
            _output = output;

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


        [InlineData("Peter", "Stormare", true)]
        [InlineData("peter", "stormare", true)]
        [InlineData("Pet    er", "Stor        mare", true)]
        [InlineData("Pe", "St", false)]
        [InlineData("P3ter", "Sto12", true)]
        [InlineData("@GOdare", "St1 ma$re", true)]
        [InlineData(null, null, false)]
        [Theory]
        public async Task UserCreatesNames_ValidateNames_ReturnsExpected(string? firstName, string? surName, bool expected)
        {
            bool actual = true;

            string? Firstname = "";
            string? Surname = "";


            var service = new UserInputValidatorService();
            Firstname = service.UserNameValidator(firstName);
            Surname = service.UserNameValidator(surName);

            if (Firstname == null || Surname == null)
            {
                actual = false;
            }

            Assert.Equal(expected, actual);
        }
        //false = email taken
        [InlineData("PeterStormare@gmail.com", false)]
        [InlineData("bATLover@gmail.com", false)]
        [InlineData("robinbertling@gmail.com", false)]

        [Theory]
        public async Task CreateadUser_HasUniqueEmail_ReturnExpected(string email, bool expected)
        {
            var response = await _client.GetFromJsonAsync<bool>("api/User/Unique-Email/" + email);

            Assert.Equal(expected, response);
        }

        [InlineData(100, 194, true)]
        [InlineData(0, 160, false)]
        [InlineData(65, null, false)]
        [InlineData(65, 500, false)]
        [InlineData(2, 160, false)]
        [Theory]
        public async Task CreatedUser_PersonalPropertiesWithinReasonableRangeOtherwise_Expected(double weight, int height, bool expected)
        {
            //a
            var createUser = new RequestCreateGymUserDTO
            {
                Firstname = "Peter",
                Surname = "Stormare",
                Gender = true,
                HeightCm = height,
                WeightKg = weight
            };

            //a
            var service = new UserInputPropertiesValidator();
            var okWeight = await service.ValidateUserWeight(weight);
            var okHeight = await service.ValidateUserHeight(height);
            _output.WriteLine(okHeight.Message);
            _output.WriteLine(okWeight.Message);

            //a
            Assert.Equal(expected, okWeight.Success && okHeight.Success);


        }

    }
}
