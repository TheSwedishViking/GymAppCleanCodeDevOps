using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Domain
{
    //Users recorded exercises, i e Johns Leg Press on the 23 sep
    public class ExerciseRecords
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GymUser User { get; set; } = null!;
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;
        public int Repetitions { get; set; }
        public double WeightKg { get; set; }
        public int Sets { get; set; }
        public DateTime DateRecorded { get; set; }
        public double UserRating { get; set; }
    }
}
