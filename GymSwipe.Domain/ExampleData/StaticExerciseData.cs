using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.ExampleData
{
    public static class StaticExerciseData
    {
        public static List<Exercise> Exercises { get; set; } = new List<Exercise>
        {
            new Exercise{
                Id = 1,
                Name = "Pushup",
                TargetAreaId = 2,
                VideoSourceLink="https://www.youtube.com/watch?v=WDIpL0pjun0"
            },
            new Exercise
            {
                Id = 2,
                Name ="Chest Fly (machine)",
                TargetAreaId= 3,
                VideoSourceLink="https://www.youtube.com/watch?v=eGjt4lk6g34"
            }

        };
    }
}
