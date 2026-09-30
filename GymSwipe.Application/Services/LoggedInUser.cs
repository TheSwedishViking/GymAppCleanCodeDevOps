using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class LoggedInUser
    {
        public GymUser CurrentUser { get; set; }
    }
}
