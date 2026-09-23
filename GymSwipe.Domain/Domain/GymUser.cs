using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymSwipe.Domain.Domain
{
    public class GymUser
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public int Gender { get; set; }
        public virtual ICollection<ExercisePlaylist> UserPlaylists { get; set; } = new List<ExercisePlaylist>();
        public virtual ICollection<ExerciseRecords> UserRecords { get; set; } = new List<ExerciseRecords>();
        public string FriendCode { get; set; }
        //NNNN-NNNN-NNNN-NNNN
        //1337-6969-4201-6767
    }
}
