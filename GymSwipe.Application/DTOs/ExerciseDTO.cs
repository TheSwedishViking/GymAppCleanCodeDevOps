using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public List<TargetAreaDTO> TargetAreaNames { get; set; } = new List<TargetAreaDTO>();
        public string Name { get; set; } = "";
        public string VideoSourceLink { get; set; } = "";
    }
}
