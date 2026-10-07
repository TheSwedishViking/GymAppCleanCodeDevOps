using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IWorkoutFacade
    {
        Task StartWorkout();
        Task<GymPlaylist> GetPlaylist(int userId);
        Task QuitWorkout();
        Task RecordUserWorkout();
    }
}
