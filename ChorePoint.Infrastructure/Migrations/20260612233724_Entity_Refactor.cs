using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Entity_Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chore_submissions_users_approved_by_user_id",
                table: "chore_submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_chore_submissions_users_user_id",
                table: "chore_submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_chores_users_user_id",
                table: "chores");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_items_users_kid_id",
                table: "shop_items");

            migrationBuilder.DropForeignKey(
                name: "FK_users_parents_parent_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "kids");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "shop_items",
                newName: "shop_item_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "parents",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "parent_settings",
                newName: "parent_settings_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "chores",
                newName: "kid_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "chores",
                newName: "chore_id");

            migrationBuilder.RenameIndex(
                name: "IX_chores_user_id",
                table: "chores",
                newName: "IX_chores_kid_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "chore_submissions",
                newName: "kid_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "chore_submissions",
                newName: "chore_submission_id");

            migrationBuilder.RenameIndex(
                name: "IX_chore_submissions_user_id",
                table: "chore_submissions",
                newName: "IX_chore_submissions_kid_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "kids",
                newName: "kid_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_parent_id",
                table: "kids",
                newName: "IX_kids_parent_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "shop_items",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "shop_items",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "parents",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "parents",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "parent_settings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "parent_settings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "chores",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "chores",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "chores",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "chores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "chore_submissions",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "chore_submissions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "chore_submissions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "kids",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "kids",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_kids",
                table: "kids",
                column: "kid_id");

            migrationBuilder.CreateIndex(
                name: "IX_chores_parent_id",
                table: "chores",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chore_submissions_kids_kid_id",
                table: "chore_submissions",
                column: "kid_id",
                principalTable: "kids",
                principalColumn: "kid_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chore_submissions_parents_approved_by_user_id",
                table: "chore_submissions",
                column: "approved_by_user_id",
                principalTable: "parents",
                principalColumn: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chores_kids_kid_id",
                table: "chores",
                column: "kid_id",
                principalTable: "kids",
                principalColumn: "kid_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chores_parents_parent_id",
                table: "chores",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "parent_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_kids_parents_parent_id",
                table: "kids",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "parent_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_items_kids_kid_id",
                table: "shop_items",
                column: "kid_id",
                principalTable: "kids",
                principalColumn: "kid_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chore_submissions_kids_kid_id",
                table: "chore_submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_chore_submissions_parents_approved_by_user_id",
                table: "chore_submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_chores_kids_kid_id",
                table: "chores");

            migrationBuilder.DropForeignKey(
                name: "FK_chores_parents_parent_id",
                table: "chores");

            migrationBuilder.DropForeignKey(
                name: "FK_kids_parents_parent_id",
                table: "kids");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_items_kids_kid_id",
                table: "shop_items");

            migrationBuilder.DropIndex(
                name: "IX_chores_parent_id",
                table: "chores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kids",
                table: "kids");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "chores");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "chore_submissions");

            migrationBuilder.RenameTable(
                name: "kids",
                newName: "users");

            migrationBuilder.RenameColumn(
                name: "shop_item_id",
                table: "shop_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "parents",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "parent_settings_id",
                table: "parent_settings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "kid_id",
                table: "chores",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "chore_id",
                table: "chores",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_chores_kid_id",
                table: "chores",
                newName: "IX_chores_user_id");

            migrationBuilder.RenameColumn(
                name: "kid_id",
                table: "chore_submissions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "chore_submission_id",
                table: "chore_submissions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_chore_submissions_kid_id",
                table: "chore_submissions",
                newName: "IX_chore_submissions_user_id");

            migrationBuilder.RenameColumn(
                name: "kid_id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_kids_parent_id",
                table: "users",
                newName: "IX_users_parent_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "shop_items",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "shop_items",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "parents",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "parent_settings",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "parent_settings",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "chores",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "chores",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "chores",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "chore_submissions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "chore_submissions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chore_submissions_users_approved_by_user_id",
                table: "chore_submissions",
                column: "approved_by_user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chore_submissions_users_user_id",
                table: "chore_submissions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chores_users_user_id",
                table: "chores",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_items_users_kid_id",
                table: "shop_items",
                column: "kid_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_parents_parent_id",
                table: "users",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
