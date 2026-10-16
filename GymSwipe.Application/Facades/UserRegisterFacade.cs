using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using System.Net.Http.Json;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class UserRegisterFacade : IUserRegisterFacade
    {
        private readonly HttpClient _httpClient;
        private readonly IUserInputValidatorService _inputVali;
        private readonly IUserInputPropertiesValidator _inputProp;
        public UserRegisterFacade(IUserInputPropertiesValidator userInputPropertiesValidator, IUserInputValidatorService userInputValidatorService, HttpClient httpClient)
        {
            _inputVali = userInputValidatorService;
            _inputProp = userInputPropertiesValidator;
            _httpClient = httpClient;
        }

        public async Task<List<string>> CheckIfEmailIsUnique(string email)
        {
            List<string> errors = new List<string>();
            var correct = _inputVali.UserEmailIsValid(email);
            if (correct.IsValid == false)
            {
                errors.Add("Email not valid");
            }
            bool uniqueEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/Unique-Email/" + correct.Email);
            if (uniqueEmail == false)
            {
                errors.Add($"{correct.Email} is taken");
            }
            return errors;
        }

        public async Task<GymUserDTO> CreateUserFromRegistration(RequestCreateGymUserDTO createGymUserDTO)
        {
            using var response = await _httpClient.PostAsJsonAsync("api/User", createGymUserDTO);
            response.EnsureSuccessStatusCode();
            //Get valid user from API if we could save
            var user = await response.Content.ReadFromJsonAsync<GymUserDTO>();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<string> GetCorrectFormatedEmail(string email)
        {
            var correctFormat = _inputVali.UserEmailIsValid(email);
            return correctFormat.Email;
        }

        public async Task<GymUserDTO> GetUserByEmail(string email)
        {
            var found = await _httpClient.GetFromJsonAsync<GymUserDTO>("api/User/Get-UserDto-By-Email/" + email);
            return found;
        }

        public async Task<List<string>> ValidateEmail(string email) // Could be broken down more with services
        {
            List<string> errors = new List<string>();

            var correctFormat = _inputVali.UserEmailIsValid(email);
            if (correctFormat.IsValid == false)
            {
                errors.Add("Bad email format");
                return errors;
            }

            bool existingEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/Unique-Email/" + email);
            if (existingEmail)
            {
                errors.Add($"An account with {email} not found! Register a new one");
                return errors;
            }
            var found = await _httpClient.GetFromJsonAsync<GymUserDTO>("api/User/Get-UserDto-By-Email/" + email);
            if (found == null)
            {
                errors.Add("User not found");
            }
            return errors;
        }

        public async Task<List<string>> ValidateUser(RequestCreateGymUserDTO createGymUserDTO)
        {
            List<string> errors = new List<string>();
            var first = _inputVali.UserNameValidator(createGymUserDTO.Firstname);
            if (first is null)
            {
                errors.Add("Invalid first name. Use 2-20 letters.");
            }
            var last = _inputVali.UserNameValidator(createGymUserDTO.Surname);
            if (last is null)
            {
                errors.Add("Invalid surname. Use 2-20 letters.");
            }
            if (string.IsNullOrEmpty(createGymUserDTO.Email))
            {
                errors.Add("Email cant be empty!");
            }
            var correctEmail = _inputVali.UserEmailIsValid(createGymUserDTO.Email);

            if (correctEmail.IsValid == false)
            {
                errors.Add("Email not valid");
            }
            var validHeight = await _inputProp.ValidateUserHeight(createGymUserDTO.HeightCm);
            if (validHeight.Success == false)
            {
                errors.Add(validHeight.Message);
            }
            var validWeight = await _inputProp.ValidateUserWeight(createGymUserDTO.WeightKg);
            if (validWeight.Success == false)
            {
                errors.Add(validWeight.Message);
            }
            return errors;
        }
    }
}
