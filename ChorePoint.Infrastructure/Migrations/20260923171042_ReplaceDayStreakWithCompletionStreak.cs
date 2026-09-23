using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDayStreakWithCompletionStreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayStreak",
                table: "Kids");

            migrationBuilder.AddColumn<int>(
                name: "CompletionStreak",
                table: "KidChore",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StreakLastUpdated",
                table: "KidChore",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionStreak",
                table: "KidChore");

            migrationBuilder.DropColumn(
                name: "StreakLastUpdated",
                table: "KidChore");

            migrationBuilder.AddColumn<int>(
                name: "DayStreak",
                table: "Kids",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
