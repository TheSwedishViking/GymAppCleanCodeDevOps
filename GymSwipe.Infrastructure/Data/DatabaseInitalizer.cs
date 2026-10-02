using GymSwipe.Domain;
using GymSwipe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Data
{
    public class DatabaseInitalizer : IDatabaseInitalizer
    {
        private readonly GymAppDbContext _db;
        public DatabaseInitalizer(GymAppDbContext gymAppDbContext)
        {
            _db = gymAppDbContext;
        }
        public async Task InitalizeAsync(CancellationToken ct = default)
        {
            //Relational ones SQL server, SSMS, in-memory dosen't count => Can't migrate
            if (_db.Database.IsRelational())
            {
                await _db.Database.MigrateAsync(ct);
            }
            //In memory awaits to ensure creation, so that we can handle tests
            else
            {
                await _db.Database.EnsureCreatedAsync();
            }
            //Assume no exercises => Db empty
            if (!await _db.Exercises.AnyAsync(ct))
            {
                var chest = new ExerciseTargetArea { ExerciseCategoryName = "Chest" };
                var biceps = new ExerciseTargetArea { ExerciseCategoryName = "Biceps" };
                var triceps = new ExerciseTargetArea { ExerciseCategoryName = "Triceps" };
                var shoulder = new ExerciseTargetArea { ExerciseCategoryName = "Shoulders" };
                var back = new ExerciseTargetArea { ExerciseCategoryName = "Back" };
                var fthighs = new ExerciseTargetArea { ExerciseCategoryName = "Front thighs" };
                var bthighs = new ExerciseTargetArea { ExerciseCategoryName = "Backside thighs" };
                var lats = new ExerciseTargetArea { ExerciseCategoryName = "Lats" };

                _db.ExercisesTargetAreas.AddRange(chest, biceps, triceps, shoulder, back, fthighs, bthighs, lats);


                _db.Exercises.AddRange(
                    new Exercise { Name = "Push up", TargetAreas = { chest, triceps, shoulder } },
                    new Exercise { Name = "Pull up", TargetAreas = { back, biceps, shoulder} },
                    new Exercise { Name = "Squat" , TargetAreas = { fthighs, bthighs } },
                    new Exercise { Name = "Bicep curl", TargetAreas = {biceps } },
                    new Exercise { Name = "Bench press", TargetAreas = { chest, triceps } }
                    );

              await  _db.SaveChangesAsync();
            }
        }
    }
}
