using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GymSwipe.ApplicationLayer.Interfaces.Validators
{
    public interface IUserInputValidatorService
    {
        public string? UserNameValidator(string? name);
        public string Capitalize(string lowerName);
        public (bool IsValid, string? Email) UserEmailIsValid(string? email);
    }
}
