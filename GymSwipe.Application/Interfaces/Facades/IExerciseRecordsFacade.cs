using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Facades
{
    public interface IExerciseRecordsFacade
    {
        Task<bool> GetActiveStatus();
        Task<List<PlaylistExerciseDTO>> GetPlaylistExercises();
        Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current);
        Task<List<ExerciseRecordDTO>> GetActiveRecords();
        Task SaveRecords(List<ExerciseRecordDTO> records);
        Task<List<string>> AddRecord(ExerciseRecordDTO currentRecord);
        Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO ex);
        Task<PlaylistExerciseDTO> GetFirstExercise(ObservableCollection<PlaylistExerciseDTO> excercises);
        Task StopEvaluation();
    }
}
