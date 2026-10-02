using GymSwipe.Domain;
using GymSwipe.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
                    new Exercise { Name = "Pull up", TargetAreas = { back, biceps, shoulder } },
                    new Exercise { Name = "Squat", TargetAreas = { fthighs, bthighs } },
                    new Exercise { Name = "Bicep curl", TargetAreas = { biceps } },
                    new Exercise { Name = "Bench press", TargetAreas = { chest, triceps } }
                    );

                await _db.SaveChangesAsync();
            }
            if (!await _db.Users.AnyAsync(ct))
            {
                var users = new List<GymUser>
                {
                                                       new() { Firstname = "jOHHNY",   Surname = "Doughie",   Email = "johnny.doughie@gmail.com",  HeightCm = 230, WeightKg = 550, Gender = true },
                                                        new() { Firstname = "David",    Surname = "Svensson",  Email = "david.svensson@gmail.com",  HeightCm = 180, WeightKg = 58,  Gender = null },
                                                        new() { Firstname = "Robin",    Surname = "Bertling",  Email = "robinbertling@gmail.com",   HeightCm = 390, WeightKg = 120, Gender = true },
                                                        new() { Firstname = "Oscar",    Surname = "Stenström", Email = "oscar.stenstrom@gmail.com", HeightCm = 150, WeightKg = 75,  Gender = true },
                                                        new() { Firstname = "Batman",   Surname = "Superbat",  Email = "batlover@gmail.com",        HeightCm = 180, WeightKg = 100, Gender = true },
                                                        new() { Firstname = "Superman", Surname = "Lane",      Email = "bathater@gmail.com",        HeightCm = 190, WeightKg = 80,  Gender = true },
                                                        new() { Firstname = "Clark",    Surname = "Kent",      Email = "clark.kent@gmail.com",      HeightCm = 190, WeightKg = 90,  Gender = true },
                                                        new() { Firstname = "Ben",      Surname = "Ten",       Email = "ben.ten@gmail.com",         HeightCm = 190, WeightKg = 100, Gender = true },
                                                        new() { Firstname = "Höga",     Surname = "Ten",       Email = "hoga.ten@gmail.com",        HeightCm = 189, WeightKg = 86,  Gender = true },
                                                        new() { Firstname = "Johhny",   Surname = "Bravo",     Email = "johhny.bravo@gmail.com",    HeightCm = 200, WeightKg = 100, Gender = true },
                                                        new() { Firstname = "Usain",    Surname = "Bolt",      Email = "PeterStormage@gmail.com",   HeightCm = 200, WeightKg = 90,  Gender = null },
                                                        new() { Firstname = "Ned",      Surname = "Stark",     Email = "Peter123Stormare@gmail.com",HeightCm = 200, WeightKg = 100, Gender = true },
                                                        new() { Firstname = "Peter",    Surname = "Stormare",  Email = "PeterStormare@gmail.com",   HeightCm = 200, WeightKg = 90,  Gender = true },

                };
                foreach (var user in users)
                {
                    _db.Users.Add(user);

                }
                await _db.SaveChangesAsync();


            }


        }

    }
}
