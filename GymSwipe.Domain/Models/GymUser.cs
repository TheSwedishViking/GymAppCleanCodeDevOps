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
        public string Firstname { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int HeightCm { get; set; }
        public double WeightKg { get; set; }
        public bool? Gender { get; set; }
        public virtual ICollection<GymPlaylist>? UserPlaylists { get; set; } = new List<GymPlaylist>();
        public virtual ICollection<ExerciseRecords>? UserRecords { get; set; } = new List<ExerciseRecords>();
        public string FriendCode { get; private set; } = "";
        public GymUser()
        {
        }
        public void SetFriendCode()
        {
            string friendCode = "";
            for(int i = 0; i<4; i++)
            {
                for(int y  = 0; y<4; y++)
                {
                    friendCode += Random.Shared.Next(0, 9);
                }
                if (i != 3)
                {
                    friendCode += "-";
                }
            }

            Console.WriteLine(friendCode);
        }
        //NNNN-NNNN-NNNN-NNNN
        //1337-6969-4201-6767
    }
}
