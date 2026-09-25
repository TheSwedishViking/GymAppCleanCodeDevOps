using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        public int TargetAreaId { get; set; }
        public ICollection< ExerciseTargetArea> TargetAreas { get; set; } = new List<ExerciseTargetArea>();
        public string Name { get; set; } = "";
        public string VideoSourceLink { get; set; } = "";
    }
}
