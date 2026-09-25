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
            //User has records, delete records when user is removed
            modelBuilder.Entity<ExerciseRecords>(exr =>
            {
                exr.HasOne(r => r.User)
                .WithMany(ex => ex.UserRecords)
                .HasForeignKey(ex => ex.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                exr.HasOne(e => e.Exercise)
                .WithMany()
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict); //Don't remove and excresive if its used, would be a bit weird => Review later if null would be better later
            });
            modelBuilder.Entity<Exercise>(e =>
            {
                e.HasMany(er => er.TargetAreas)
                .WithMany(er => er.Exercises);
                //.OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GymUser>()
           .HasIndex(u => u.FriendCode)
           .IsUnique(); //Needs to be validated and tested later, should never repeat : Must always be unique

            modelBuilder.Entity<GymPlaylist>(g =>
            {
                g.HasOne(gm => gm.User)
                .WithMany(gm => gm.UserPlaylists)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
