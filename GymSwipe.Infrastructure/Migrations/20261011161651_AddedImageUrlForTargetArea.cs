using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSwipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlForTargetArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageOfTargetArea",
                table: "ExercisesTargetAreas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOfTargetArea",
                table: "ExercisesTargetAreas");
        }
    }
}
