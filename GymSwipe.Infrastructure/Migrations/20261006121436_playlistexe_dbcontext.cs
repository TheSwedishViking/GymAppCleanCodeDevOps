using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSwipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class playlistexe_dbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaylistExercises_GymPlaylists_GymPlaylistId",
                table: "UserPlaylistExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserPlaylistExercises_GymPlaylistId",
                table: "UserPlaylistExercises");

            migrationBuilder.DropColumn(
                name: "GymPlaylistId",
                table: "UserPlaylistExercises");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlaylistExercises_ExerciseId",
                table: "UserPlaylistExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlaylistExercises_PlaylistId",
                table: "UserPlaylistExercises",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaylistExercises_Exercises_ExerciseId",
                table: "UserPlaylistExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaylistExercises_GymPlaylists_PlaylistId",
                table: "UserPlaylistExercises",
                column: "PlaylistId",
                principalTable: "GymPlaylists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaylistExercises_Exercises_ExerciseId",
                table: "UserPlaylistExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaylistExercises_GymPlaylists_PlaylistId",
                table: "UserPlaylistExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserPlaylistExercises_ExerciseId",
                table: "UserPlaylistExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserPlaylistExercises_PlaylistId",
                table: "UserPlaylistExercises");

            migrationBuilder.AddColumn<int>(
                name: "GymPlaylistId",
                table: "UserPlaylistExercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPlaylistExercises_GymPlaylistId",
                table: "UserPlaylistExercises",
                column: "GymPlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaylistExercises_GymPlaylists_GymPlaylistId",
                table: "UserPlaylistExercises",
                column: "GymPlaylistId",
                principalTable: "GymPlaylists",
                principalColumn: "Id");
        }
    }
}
