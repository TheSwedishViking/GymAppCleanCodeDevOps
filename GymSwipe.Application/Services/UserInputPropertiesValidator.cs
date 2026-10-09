namespace GymSwipe.ApplicationLayer.Services
{
    public class UserInputPropertiesValidator
    {

        public async Task<(bool Success, string Message)> ValidateUserWeight(double weight)
        {
            if (weight == null || weight <= 49 || weight >= 251)
            {
                return (false, "Invalid weight. Please enter a weight between 50 and 250 kg.");
            }

            return (true, "Weight is valid.");
        }

        public async Task<(bool Success, string Message)> ValidateUserHeight(int height)
        {
            if (height == null || height <= 129 || height >= 251)
            {
                return (false, "Invalid height. Please enter a height between 130 and 250 cm.");
            }
            return (true, "Height is valid.");
        }

    }
}
