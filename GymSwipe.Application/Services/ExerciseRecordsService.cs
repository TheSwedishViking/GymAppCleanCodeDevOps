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
        private readonly IMapDomainService _mapDomainService;
        public ExerciseRecordsService(IExerciseRecordsRepository recordsRepository, IMapDomainService mapDomainService)
        {
            _repo = recordsRepository;
            _mapDomainService = mapDomainService;
        }

        public async Task SaveRecordsFromCurrentPlaylist(List<ExerciseRecordDTO> records)
        {
            List<ExerciseRecord> domains = (await Task.WhenAll(records.Select(_mapDomainService.MapExerciseRecordDomain))).ToList();

            await _repo.SaveRecords(domains);
        }
       
    }
}
