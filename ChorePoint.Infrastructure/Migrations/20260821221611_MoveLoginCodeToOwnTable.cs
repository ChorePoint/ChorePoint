using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveLoginCodeToOwnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginCode",
                table: "Kids");

            migrationBuilder.CreateTable(
                name: "LoginCodes",
                columns: table => new
                {
                    KidId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginCodes", x => x.KidId);
                    table.ForeignKey(
                        name: "FK_LoginCodes_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "KidId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginCodes");

            migrationBuilder.AddColumn<string>(
                name: "LoginCode",
                table: "Kids",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);
        }
    }
}
