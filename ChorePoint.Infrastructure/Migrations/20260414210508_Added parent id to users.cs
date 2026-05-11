using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedparentidtousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_parent_id",
                table: "users",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_parents_parent_id",
                table: "users",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_parents_parent_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_parent_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "users");
        }
    }
}
