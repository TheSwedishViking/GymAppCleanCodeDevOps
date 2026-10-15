using GymSwipe.ApplicationLayer.Interfaces.Validators;
using System.Text.RegularExpressions;

namespace GymSwipe.ApplicationLayer.Services.Validators
{
    public class UserInputValidatorService : IUserInputValidatorService
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
            Regex namePattern = new Regex(@"^([a-åäö]{2,21})$");

            string lowerName = trimmedName.ToLower();
            if (!namePattern.IsMatch(lowerName))
            {
                return null;
            }
            return Capitalize(lowerName);
        }

        public  string Capitalize(string lowerName)
        {
            return char.ToUpper(lowerName[0]) + lowerName.Substring(1);
        }

        public (bool IsValid, string? Email) UserEmailIsValid(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return (false, null);
            }
            string emailLower = email.ToLower();
            Regex emailPattern = new Regex(@"^[a-åäö0-9]{3,40}@[a-z0-9]{1,15}\.[a-åäö]{2,6}$");
            if (!emailPattern.IsMatch(emailLower))
            {
                emailLower = "Email incorrect format. ex JohnDoe@gmail.com 40 letters and numbers for name and a valid email provider"; //response message
                return (false, emailLower);
            }

            return (true, emailLower); //emailValid

        }




    }
}