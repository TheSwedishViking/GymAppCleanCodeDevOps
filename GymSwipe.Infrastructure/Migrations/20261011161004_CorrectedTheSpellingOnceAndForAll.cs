using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSwipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedTheSpellingOnceAndForAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExerciseRecords_UserId",
                table: "ExerciseRecords");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRecords_UserId_ExerciseId_DateRecorded",
                table: "ExerciseRecords",
                columns: new[] { "UserId", "ExerciseId", "DateRecorded" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExerciseRecords_UserId_ExerciseId_DateRecorded",
                table: "ExerciseRecords");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRecords_UserId",
                table: "ExerciseRecords",
                column: "UserId");
        }
    }
}
