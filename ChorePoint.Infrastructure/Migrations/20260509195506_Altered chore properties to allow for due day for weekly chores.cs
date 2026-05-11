using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alteredchorepropertiestoallowforduedayforweeklychores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_date",
                table: "chores");

            migrationBuilder.DropColumn(
                name: "time_of_day",
                table: "chores");

            migrationBuilder.AddColumn<int>(
                name: "due_day",
                table: "chores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_day",
                table: "chores");

            migrationBuilder.AddColumn<int>(
                name: "due_date",
                table: "chores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "time_of_day",
                table: "chores",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
