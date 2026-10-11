using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseRecordsService : IExerciseRecordsService
    {
        private readonly IExerciseRecordsRepository _repo;
        public ExerciseRecordsService(IExerciseRecordsRepository recordsRepository)
        {
            _repo = recordsRepository;
        }
        public Task<ExerciseRecord> GetAExerciseRecordsAsync(int exerciseId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveRecordsFromCurrentPlaylist(int userId, GymPlaylist usersPlaylist)
        {
            throw new NotImplementedException();
        }

        public async Task SaveRecordsFromCurrentPlaylist(List<ExerciseRecordDTO> records)
        {
            List<ExerciseRecord> domains = (await Task.WhenAll(records.Select(DtoToDomain))).ToList();

            await _repo.SaveRecords(domains);
        }
        public async Task<ExerciseRecord> DtoToDomain(ExerciseRecordDTO dTO)
        {
            return new ExerciseRecord
            {
                ExerciseId = dTO.ExerciseId,
                UserId = dTO.UserId,
                WeightKg = dTO.WeightKg,
                Repetitions = dTO.Repetitions,
                Sets = dTO.Sets,
                UserRating = dTO.UserRating,
                DateRecorded = dTO.DateRecorded,
            };
        }
    }
}
