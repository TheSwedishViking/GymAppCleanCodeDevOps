using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSwipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastNeededFieldsForSprintThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CaloriesBurnedFromLatestWorkout",
                table: "GymPlaylists",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPlayed",
                table: "GymPlaylists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LatestElapsedTime",
                table: "GymPlaylists",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesBurnedFromLatestWorkout",
                table: "GymPlaylists");

            migrationBuilder.DropColumn(
                name: "LastPlayed",
                table: "GymPlaylists");

            migrationBuilder.DropColumn(
                name: "LatestElapsedTime",
                table: "GymPlaylists");
        }
    }
}
