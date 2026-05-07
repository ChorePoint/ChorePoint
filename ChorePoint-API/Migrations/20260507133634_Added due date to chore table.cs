using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint_API.Migrations
{
    /// <inheritdoc />
    public partial class Addedduedatetochoretable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "due_date",
                table: "chores",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "due_date",
                table: "chores");
        }
    }
}
