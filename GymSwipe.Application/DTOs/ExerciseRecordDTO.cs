using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.DTOs
{
    public class ExerciseRecordDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public int Repetitions { get; set; }
        public double WeightKg { get; set; }
        public int Sets { get; set; }
        public DateTime DateRecorded { get; set; }
        public double UserRating { get; set; }
    }
}
