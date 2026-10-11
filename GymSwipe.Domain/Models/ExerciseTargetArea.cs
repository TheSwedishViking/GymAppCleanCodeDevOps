using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Models
{
    public class ExerciseTargetArea
    {
        public int Id { get; set; }
        public string ExerciseCategoryName { get; set; } = "";        //Glutes, biceps etc, common name
        public string? ImageOfTargetArea { get; set; } = ""; //glutes.png etc
        //Be able to query all relevant exercises for glutes in the future
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
