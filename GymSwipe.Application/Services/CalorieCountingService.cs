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
        public double CalculateCalories(TimeSpan time, double weightKg, double? metFactor = ModerateMetExerciseFactor)
        {
            //Based on this https://www.calculator.net/calories-burned-calculator.html?activity=2&activity2=Rowing%2C+Stationary%3A+moderate&chour=&cmin=30&cweight=82&cweightunit=k&ctype=1&x=Calculate
            //If > 0 has value, otherwise moderate
            double met = metFactor is > 0 ? metFactor.Value : ModerateMetExerciseFactor;

            // kcal/min = MET * 3.5 * weightKg / 200
            double calories = time.TotalMinutes * 3.5 * weightKg / 200;

            return calories;
        }

    }
}
