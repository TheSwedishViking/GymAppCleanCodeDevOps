using GymSwipe.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public int TargetAreaId { get; set; }
        public string TargetAreaName { get; set; }
        public string Name { get; set; } = "";
        public string VideoSourceLink { get; set; } = "";
    }
}
