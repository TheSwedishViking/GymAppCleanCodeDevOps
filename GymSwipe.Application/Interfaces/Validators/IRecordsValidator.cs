using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Validators
{
    public interface IRecordsValidator
    {
        List<string> Validate(ExerciseRecordDTO currentRecord);
    }
}
