using System.Text.RegularExpressions;

namespace GymSwipe.ApplicationLayer.Services
{
    public class UserInputValidatorService
    {
        public string? UserNameValidator(string name)
        {
            string trimmedName = "";


            foreach (char c in name)
            {
                if (char.IsLetter(c))
                {
                    trimmedName += c;
                }
            }
            //Titta på läng imorgon
            Regex namePattern = new Regex(@"^([a-z]{3,15})$");


            namePattern.IsMatch(trimmedName.ToLower());

            return Capitalize(trimmedName);
        }

        public static string Capitalize(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}