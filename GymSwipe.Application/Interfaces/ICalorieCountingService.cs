using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ICalorieCountingService
    {
        Task<double> CalculateCalories(TimeSpan time, double weightKg, double metFactor);
        double GetMETFactor();
    }
}
