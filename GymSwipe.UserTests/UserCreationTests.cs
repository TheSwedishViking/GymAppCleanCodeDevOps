using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Interfaces;
using GymSwipe.Domain.Models;
using Xunit;

namespace GymSwipe.UserTests
{
    public class UserCreationTests
    {

        IUserRepository _userRepository;

        public UserCreationTests(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Fact]
        public async Task TryAndCreateUser_WithEmptyDate_ReturnsExpectedInvalidResult()
        {
            RequestCreateGymUserDTO badUserData = new RequestCreateGymUserDTO
            {
                Firstname = "1231312313",
                Surname = "1313123132",
                Email = "invalidemailformat",
                Gender = null,
                HeightCm = -5123,
                Password = "password",
                WeightKg = -50000
            };

            var sut = new UserService();
            var result = await sut.TryAndCreateUserThroughRequestModelAsync(badUserData);

            Assert.Null(result);
        }

        [Fact]
        public async Task TryToCreateAUserAndSaveItToDb_ReturnsExcpected()
        {
            //a
            GymUserDTO user = new()
            {
                Firstname = "Fred",
                Surname = "Davidson",
                Email = "FredDavidson@gmail.com",
                Gender = true,
                HeightCm = 180,
                WeightKg = 100
            };

            //a
            GymUser.CurrentUser.Firstname = user.Firstname;
            GymUser.CurrentUser.Surname = user.Surname;
            GymUser.CurrentUser.Email = user.Email;
            GymUser.CurrentUser.Gender = user.Gender;
            GymUser.CurrentUser.HeightCm = user.HeightCm;
            GymUser.CurrentUser.WeightKg = user.WeightKg;

            _userRepository.AddUser(GymUser.CurrentUser);

            var retrievedUser = await _userRepository.GetUserById(GymUser.CurrentUser.Id);
            //a
            Assert.Equal(user.Firstname, retrievedUser.Firstname);

        }
    }
}
