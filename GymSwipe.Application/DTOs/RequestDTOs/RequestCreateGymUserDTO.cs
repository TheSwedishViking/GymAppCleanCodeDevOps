using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.DTOs.RequestDTOs
{
    public class RequestCreateGymUserDTO
    {
        //Lacks ID and friendcode, facade returns a valid DTO with those properties
        public string Firstname { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public int HeightCm { get; set; }
        public double WeightKg { get; set; }
        public bool? Gender { get; set; }
    }
}
