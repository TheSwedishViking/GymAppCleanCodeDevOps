using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.ExampleData
{
    public static class StaticTargetAreas
    {
        public static List<ExerciseTargetArea> ExerciseTargetAreas { get; set; } = new List<ExerciseTargetArea>
        {
            new ExerciseTargetArea
            {
                Id = 1,
                ExerciseCategoryName="Back",
            },
            new ExerciseTargetArea
            {
                Id=2,
                ExerciseCategoryName="Shoulders"
            },
            new ExerciseTargetArea
            {
                Id=3, ExerciseCategoryName="Chest"
            }
        };
    }
}
