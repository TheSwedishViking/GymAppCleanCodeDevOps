using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Models
{
    public class ExerciseTargetArea
    {
        public int Id { get; set; }
        //Glutes, biceps etc
        public string ExerciseCategoryName { get; set; } = "";
        //Be able to query all relevant exercises for glutes in the future
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
