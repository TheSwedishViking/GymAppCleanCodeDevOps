using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsFacade
    {
        Task<bool> GetActiveStatus();
        Task<List<PlaylistExercise>> GetPlaylistExercises();
        Task<PlaylistExercise> GetNextExercise(PlaylistExercise current);
        Task<List<ExerciseRecordDTO>> GetActiveRecords();
        Task<bool> SaveRecords(List<ExerciseRecordDTO> records);
        Task AddRecord(ExerciseRecordDTO currentRecord);
        Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise ex);
        Task<PlaylistExercise> GetFirstExercise(ObservableCollection<PlaylistExercise> excercises);
    }
}
