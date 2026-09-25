using GymSwipe.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Data
{
    public class GymAppDbContext:DbContext
    {
        private readonly string _connstring = "Not handled yet";

        public GymAppDbContext(DbContextOptions<GymAppDbContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseRecords> ExerciseRecords { get; set; }
        public DbSet<ExerciseTargetArea> ExercisesTargetAreas { get; set; }
        public DbSet<GymPlaylist> GymPlaylists { get; set; }
        public DbSet<GymUser> Users { get; set; }
        public DbSet<PlaylistExcercise> UserPlaylistExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
