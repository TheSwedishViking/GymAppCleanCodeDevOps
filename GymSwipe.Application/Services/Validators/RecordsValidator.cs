using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.Validators
{
    public class RecordsValidator : IRecordsValidator
    {
        public List<string> Validate(ExerciseRecordDTO currentRecord)
        {
            List<string> errors = new List<string>();

            if (currentRecord.WeightKg is < 0 or > 300)
            {
                errors.Add("Weight must be between 0 and 300 kg.");
            }

            if (currentRecord.Repetitions is < 1 or > 100)
            {
                errors.Add("Repitions within reasonable values (1-100).");

            }
            if (currentRecord.Sets is < 1 or > 100)
            {
                errors.Add("Sets within reasonable values (1-100).");
            }
            return errors;
        }
    }
}
