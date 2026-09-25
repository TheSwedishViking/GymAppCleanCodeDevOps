using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class UserCreationTests
    {
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
    }
}
