using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Validators
{
    public interface IUserInputPropertiesValidator
    {
         Task<(bool Success, string Message)> ValidateUserWeight(double weight);

         Task<(bool Success, string Message)> ValidateUserHeight(int height);
        
    }
}
