using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;

namespace TestCmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        var request = new RequestCreateGymUserDTO
        {
            Firstname = "PETER",
            Surname = "Stormare",
            Gender = true,
            HeightCm = 200,
            WeightKg = 90
        };

        var service = new UserInputValidatorService();
        request.Firstname = service.UserNameValidator(request.Firstname);

            var response = await _client.PostAsJsonAsync("api/User", request);

        response.EnsureSuccessStatusCode();
            var returnedUser = await response.Content.ReadFromJsonAsync<GymUserDTO>();
    }
}
}
