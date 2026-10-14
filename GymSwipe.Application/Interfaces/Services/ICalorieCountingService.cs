using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Services
{
    public interface ICalorieCountingService
    {
        double CalculateCalories(TimeSpan time, double weightKg, double? metFactor);
    }
}
