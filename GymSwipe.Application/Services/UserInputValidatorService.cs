using System.Text.RegularExpressions;

namespace GymSwipe.ApplicationLayer.Services
{
    public class UserInputValidatorService
    {
        public string? UserNameValidator(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            string? trimmedName = "";


            foreach (char c in name)
            {
                if (char.IsLetter(c))
                {
                    trimmedName += c;
                }
            }
            //Titta på läng imorgon
            Regex namePattern = new Regex(@"^([a-z]{3,15})$");

            string lowerName = trimmedName.ToLower();
            if (!namePattern.IsMatch(lowerName))
            {
                return null;
            }
            return Capitalize(lowerName);
        }

        public static string Capitalize(string lowerName)
        {
            return char.ToUpper(lowerName[0]) + lowerName.Substring(1);
        }
    }
}