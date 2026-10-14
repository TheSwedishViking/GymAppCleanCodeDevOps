using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class CalorieCountingService : ICalorieCountingService
    {
        public const double ModerateMetExerciseFactor = 5.0;
        public double MetFactor => GetMETFactor();
        public async Task<double> CalculateCalories(TimeSpan time, double weightKg, double? metFactor = ModerateMetExerciseFactor)
        {
            //Based on this https://www.calculator.net/calories-burned-calculator.html?activity=2&activity2=Rowing%2C+Stationary%3A+moderate&chour=&cmin=30&cweight=82&cweightunit=k&ctype=1&x=Calculate

            if(metFactor == 0 || metFactor < 0||!metFactor.HasValue)
            {
                metFactor = MetFactor;
            }
            double calories = time.TotalMinutes * metFactor.Value * weightKg;

            return calories;
        }

        public double GetMETFactor()
        {
            return (ModerateMetExerciseFactor * 3.5)/200;
        }
    }
}
