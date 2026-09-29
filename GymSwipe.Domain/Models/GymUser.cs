using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymSwipe.Domain.Models
{
    public class GymUser : INotifyPropertyChanged
    {
        public static GymUser CurrentUser { get; } = new GymUser();

        [Key]
        public int Id { get; set; }

        //Reload page where name gets new value
        private string _firstname = "";
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname == value) return;
                _firstname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Firstname)));
            }
        }
        private string _surname = "";
        public string Surname
        {
            get => _surname;
            set
            {
                if (_surname == value) return;
                _surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int HeightCm { get; set; }
        public double WeightKg { get; set; }
        public bool? Gender { get; set; }
        public virtual ICollection<GymPlaylist>? UserPlaylists { get; set; } = new List<GymPlaylist>();
        public virtual ICollection<ExerciseRecords>? UserRecords { get; set; } = new List<ExerciseRecords>();
        public string FriendCode { get; private set; } = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        private GymUser()
        {
        }


        public void SetFriendCode()
        {
            string friendCode = "";
            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    friendCode += Random.Shared.Next(0, 9);
                }
                if (i != 3)
                {
                    friendCode += "-";
                }
            }
            CurrentUser.FriendCode = friendCode;
            Console.WriteLine(friendCode);
            //NNNN-NNNN-NNNN-NNNN
            //1337-6969-4201-6767


        }

    }
}
