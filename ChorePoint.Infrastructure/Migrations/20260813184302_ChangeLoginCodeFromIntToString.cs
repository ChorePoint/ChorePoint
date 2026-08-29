using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLoginCodeFromIntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LoginCode",
                table: "Kids",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LoginCode",
                table: "Kids",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
