using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.SessionServices
{
    public class WorkoutSession
    {
        public GymPlaylist CurrentPlaylist { get; private set; }
        public List<ExerciseRecords> Records { get; } = new List<ExerciseRecords>();
        public bool IsActive => CurrentPlaylist != null;

        public void Start(GymPlaylist playlist)
        {
            CurrentPlaylist = playlist;
        }
        public void Clear()
        {
            CurrentPlaylist = null;
            Records.Clear();
        }
    }
}
