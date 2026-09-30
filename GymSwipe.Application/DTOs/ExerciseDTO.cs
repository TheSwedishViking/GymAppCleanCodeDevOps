using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public List<string> TargetAreaNames { get; set; }
        public string Name { get; set; } = "";
        public string VideoSourceLink { get; set; } = "";
    }
}
