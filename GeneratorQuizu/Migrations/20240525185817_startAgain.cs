using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneratorQuizu.Migrations
{
    /// <inheritdoc />
    public partial class startAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswers1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswers2",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswers3",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswers4",
                table: "Questions",
                newName: "CorrectAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswers",
                table: "Questions",
                newName: "CorrectAnswers4");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswers1",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswers2",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswers3",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
