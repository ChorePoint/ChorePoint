using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChorePoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BigRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chore_submissions_chores_chore_id",
                table: "chore_submissions");

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
                name: "FK_parent_settings_parents_parent_id",
                table: "parent_settings");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_items_kids_kid_id",
                table: "shop_items");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_items_parents_parent_id",
                table: "shop_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parents",
                table: "parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kids",
                table: "kids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chores",
                table: "chores");

            migrationBuilder.DropIndex(
                name: "IX_chores_kid_id",
                table: "chores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shop_items",
                table: "shop_items");

            migrationBuilder.DropIndex(
                name: "IX_shop_items_kid_id",
                table: "shop_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parent_settings",
                table: "parent_settings");

            migrationBuilder.DropIndex(
                name: "IX_parent_settings_parent_id",
                table: "parent_settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chore_submissions",
                table: "chore_submissions");

            migrationBuilder.DropIndex(
                name: "IX_chore_submissions_approved_by_user_id",
                table: "chore_submissions");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "chores");

            migrationBuilder.DropColumn(
                name: "kid_id",
                table: "chores");

            migrationBuilder.DropColumn(
                name: "kid_id",
                table: "shop_items");

            migrationBuilder.DropColumn(
                name: "approved_by_user_id",
                table: "chore_submissions");

            migrationBuilder.RenameTable(
                name: "parents",
                newName: "Parents");

            migrationBuilder.RenameTable(
                name: "kids",
                newName: "Kids");

            migrationBuilder.RenameTable(
                name: "chores",
                newName: "Chores");

            migrationBuilder.RenameTable(
                name: "shop_items",
                newName: "ShopItems");

            migrationBuilder.RenameTable(
                name: "parent_settings",
                newName: "ParentSettings");

            migrationBuilder.RenameTable(
                name: "chore_submissions",
                newName: "ChoreSubmissions");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Parents",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Parents",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Parents",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Parents",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Parents",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Parents",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "Parents",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Kids",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "Kids",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Kids",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Kids",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "spendable_points",
                table: "Kids",
                newName: "SpendablePoints");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "Kids",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "day_streak",
                table: "Kids",
                newName: "DayStreak");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Kids",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "kid_id",
                table: "Kids",
                newName: "KidId");

            migrationBuilder.RenameColumn(
                name: "total_points",
                table: "Kids",
                newName: "LoginCode");

            migrationBuilder.RenameColumn(
                name: "points_today",
                table: "Kids",
                newName: "LifetimePoints");

            migrationBuilder.RenameIndex(
                name: "IX_kids_parent_id",
                table: "Kids",
                newName: "IX_Kids_ParentId");

            migrationBuilder.RenameColumn(
                name: "points",
                table: "Chores",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Chores",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "icon",
                table: "Chores",
                newName: "Icon");

            migrationBuilder.RenameColumn(
                name: "frequency",
                table: "Chores",
                newName: "Frequency");

            migrationBuilder.RenameColumn(
                name: "difficulty",
                table: "Chores",
                newName: "Difficulty");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Chores",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Chores",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "Chores",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "last_completed_at",
                table: "Chores",
                newName: "LastCompletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Chores",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "completion_count",
                table: "Chores",
                newName: "CompletionCount");

            migrationBuilder.RenameColumn(
                name: "chore_id",
                table: "Chores",
                newName: "ChoreId");

            migrationBuilder.RenameColumn(
                name: "due_day",
                table: "Chores",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_chores_parent_id",
                table: "Chores",
                newName: "IX_Chores_ParentId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ShopItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ShopItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ShopItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "ShopItems",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ShopItems",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "ShopItems",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ShopItems",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "shop_item_id",
                table: "ShopItems",
                newName: "ShopItemId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "ShopItems",
                newName: "Icon");

            migrationBuilder.RenameIndex(
                name: "IX_shop_items_parent_id",
                table: "ShopItems",
                newName: "IX_ShopItems_ParentId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ParentSettings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "shop_opening_days",
                table: "ParentSettings",
                newName: "ShopOpeningDays");

            migrationBuilder.RenameColumn(
                name: "require_photo_evidence",
                table: "ParentSettings",
                newName: "RequirePhotoEvidence");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "ParentSettings",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ParentSettings",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "auto_approve_chores",
                table: "ParentSettings",
                newName: "AutoApproveChores");

            migrationBuilder.RenameColumn(
                name: "approve_purchases",
                table: "ParentSettings",
                newName: "ApprovePurchases");

            migrationBuilder.RenameColumn(
                name: "parent_settings_id",
                table: "ParentSettings",
                newName: "ParentSettingsId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ChoreSubmissions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "kid_id",
                table: "ChoreSubmissions",
                newName: "KidId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ChoreSubmissions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "completed_at",
                table: "ChoreSubmissions",
                newName: "CompletedAt");

            migrationBuilder.RenameColumn(
                name: "chore_id",
                table: "ChoreSubmissions",
                newName: "ChoreId");

            migrationBuilder.RenameColumn(
                name: "approval_status",
                table: "ChoreSubmissions",
                newName: "ApprovalStatus");

            migrationBuilder.RenameColumn(
                name: "chore_submission_id",
                table: "ChoreSubmissions",
                newName: "ChoreSubmissionId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "ChoreSubmissions",
                newName: "ReviewNotes");

            migrationBuilder.RenameColumn(
                name: "approved_at",
                table: "ChoreSubmissions",
                newName: "ReviewedAt");

            migrationBuilder.RenameIndex(
                name: "IX_chore_submissions_kid_id",
                table: "ChoreSubmissions",
                newName: "IX_ChoreSubmissions_KidId");

            migrationBuilder.RenameIndex(
                name: "IX_chore_submissions_chore_id",
                table: "ChoreSubmissions",
                newName: "IX_ChoreSubmissions_ChoreId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Parents",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ShopItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ShopItems",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ShopItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ChoreSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parents",
                table: "Parents",
                column: "ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kids",
                table: "Kids",
                column: "KidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chores",
                table: "Chores",
                column: "ChoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems",
                column: "ShopItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentSettings",
                table: "ParentSettings",
                column: "ParentSettingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChoreSubmissions",
                table: "ChoreSubmissions",
                column: "ChoreSubmissionId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KidChore",
                columns: table => new
                {
                    KidId = table.Column<int>(type: "int", nullable: false),
                    ChoreId = table.Column<int>(type: "int", nullable: false),
                    DueDay = table.Column<int>(type: "int", nullable: true),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidChore", x => new { x.ChoreId, x.KidId });
                    table.ForeignKey(
                        name: "FK_KidChore_Chores_ChoreId",
                        column: x => x.ChoreId,
                        principalTable: "Chores",
                        principalColumn: "ChoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KidChore_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "KidId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KidShopItem",
                columns: table => new
                {
                    KidId = table.Column<int>(type: "int", nullable: false),
                    ShopItemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidShopItem", x => new { x.KidId, x.ShopItemId });
                    table.ForeignKey(
                        name: "FK_KidShopItem_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "KidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KidShopItem_ShopItems_ShopItemId",
                        column: x => x.ShopItemId,
                        principalTable: "ShopItems",
                        principalColumn: "ShopItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chores_CategoryId",
                table: "Chores",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_CategoryId",
                table: "ShopItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentSettings_ParentId",
                table: "ParentSettings",
                column: "ParentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChoreSubmissions_ParentId",
                table: "ChoreSubmissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_KidChore_KidId",
                table: "KidChore",
                column: "KidId");

            migrationBuilder.CreateIndex(
                name: "IX_KidShopItem_ShopItemId",
                table: "KidShopItem",
                column: "ShopItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreSubmissions_Chores_ChoreId",
                table: "ChoreSubmissions",
                column: "ChoreId",
                principalTable: "Chores",
                principalColumn: "ChoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreSubmissions_Kids_KidId",
                table: "ChoreSubmissions",
                column: "KidId",
                principalTable: "Kids",
                principalColumn: "KidId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreSubmissions_Parents_ParentId",
                table: "ChoreSubmissions",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chores_Categories_CategoryId",
                table: "Chores",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chores_Parents_ParentId",
                table: "Chores",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kids_Parents_ParentId",
                table: "Kids",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentSettings_Parents_ParentId",
                table: "ParentSettings",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Parents_ParentId",
                table: "ShopItems",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChoreSubmissions_Chores_ChoreId",
                table: "ChoreSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreSubmissions_Kids_KidId",
                table: "ChoreSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreSubmissions_Parents_ParentId",
                table: "ChoreSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Chores_Categories_CategoryId",
                table: "Chores");

            migrationBuilder.DropForeignKey(
                name: "FK_Chores_Parents_ParentId",
                table: "Chores");

            migrationBuilder.DropForeignKey(
                name: "FK_Kids_Parents_ParentId",
                table: "Kids");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentSettings_Parents_ParentId",
                table: "ParentSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Parents_ParentId",
                table: "ShopItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "KidChore");

            migrationBuilder.DropTable(
                name: "KidShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parents",
                table: "Parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kids",
                table: "Kids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chores",
                table: "Chores");

            migrationBuilder.DropIndex(
                name: "IX_Chores_CategoryId",
                table: "Chores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems");

            migrationBuilder.DropIndex(
                name: "IX_ShopItems_CategoryId",
                table: "ShopItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentSettings",
                table: "ParentSettings");

            migrationBuilder.DropIndex(
                name: "IX_ParentSettings_ParentId",
                table: "ParentSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChoreSubmissions",
                table: "ChoreSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_ChoreSubmissions_ParentId",
                table: "ChoreSubmissions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ChoreSubmissions");

            migrationBuilder.RenameTable(
                name: "Parents",
                newName: "parents");

            migrationBuilder.RenameTable(
                name: "Kids",
                newName: "kids");

            migrationBuilder.RenameTable(
                name: "Chores",
                newName: "chores");

            migrationBuilder.RenameTable(
                name: "ShopItems",
                newName: "shop_items");

            migrationBuilder.RenameTable(
                name: "ParentSettings",
                newName: "parent_settings");

            migrationBuilder.RenameTable(
                name: "ChoreSubmissions",
                newName: "chore_submissions");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "parents",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "parents",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "parents",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "parents",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "parents",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "parents",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "parents",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "kids",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "kids",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "kids",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "kids",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SpendablePoints",
                table: "kids",
                newName: "spendable_points");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "kids",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "DayStreak",
                table: "kids",
                newName: "day_streak");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "kids",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "KidId",
                table: "kids",
                newName: "kid_id");

            migrationBuilder.RenameColumn(
                name: "LoginCode",
                table: "kids",
                newName: "total_points");

            migrationBuilder.RenameColumn(
                name: "LifetimePoints",
                table: "kids",
                newName: "points_today");

            migrationBuilder.RenameIndex(
                name: "IX_Kids_ParentId",
                table: "kids",
                newName: "IX_kids_parent_id");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "chores",
                newName: "points");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "chores",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "chores",
                newName: "icon");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "chores",
                newName: "frequency");

            migrationBuilder.RenameColumn(
                name: "Difficulty",
                table: "chores",
                newName: "difficulty");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "chores",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "chores",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "chores",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "LastCompletedAt",
                table: "chores",
                newName: "last_completed_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "chores",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompletionCount",
                table: "chores",
                newName: "completion_count");

            migrationBuilder.RenameColumn(
                name: "ChoreId",
                table: "chores",
                newName: "chore_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "chores",
                newName: "due_day");

            migrationBuilder.RenameIndex(
                name: "IX_Chores_ParentId",
                table: "chores",
                newName: "IX_chores_parent_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "shop_items",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "shop_items",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "shop_items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "shop_items",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "shop_items",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "shop_items",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "shop_items",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ShopItemId",
                table: "shop_items",
                newName: "shop_item_id");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "shop_items",
                newName: "status");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItems_ParentId",
                table: "shop_items",
                newName: "IX_shop_items_parent_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "parent_settings",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ShopOpeningDays",
                table: "parent_settings",
                newName: "shop_opening_days");

            migrationBuilder.RenameColumn(
                name: "RequirePhotoEvidence",
                table: "parent_settings",
                newName: "require_photo_evidence");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "parent_settings",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "parent_settings",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AutoApproveChores",
                table: "parent_settings",
                newName: "auto_approve_chores");

            migrationBuilder.RenameColumn(
                name: "ApprovePurchases",
                table: "parent_settings",
                newName: "approve_purchases");

            migrationBuilder.RenameColumn(
                name: "ParentSettingsId",
                table: "parent_settings",
                newName: "parent_settings_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "chore_submissions",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "KidId",
                table: "chore_submissions",
                newName: "kid_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "chore_submissions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "chore_submissions",
                newName: "completed_at");

            migrationBuilder.RenameColumn(
                name: "ChoreId",
                table: "chore_submissions",
                newName: "chore_id");

            migrationBuilder.RenameColumn(
                name: "ApprovalStatus",
                table: "chore_submissions",
                newName: "approval_status");

            migrationBuilder.RenameColumn(
                name: "ChoreSubmissionId",
                table: "chore_submissions",
                newName: "chore_submission_id");

            migrationBuilder.RenameColumn(
                name: "ReviewedAt",
                table: "chore_submissions",
                newName: "approved_at");

            migrationBuilder.RenameColumn(
                name: "ReviewNotes",
                table: "chore_submissions",
                newName: "notes");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreSubmissions_KidId",
                table: "chore_submissions",
                newName: "IX_chore_submissions_kid_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreSubmissions_ChoreId",
                table: "chore_submissions",
                newName: "IX_chore_submissions_chore_id");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "parents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "parents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "chores",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "kid_id",
                table: "chores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "shop_items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "shop_items",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "shop_items",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "kid_id",
                table: "shop_items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "approved_by_user_id",
                table: "chore_submissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_parents",
                table: "parents",
                column: "parent_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kids",
                table: "kids",
                column: "kid_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chores",
                table: "chores",
                column: "chore_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_items",
                table: "shop_items",
                column: "shop_item_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parent_settings",
                table: "parent_settings",
                column: "parent_settings_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chore_submissions",
                table: "chore_submissions",
                column: "chore_submission_id");

            migrationBuilder.CreateIndex(
                name: "IX_chores_kid_id",
                table: "chores",
                column: "kid_id");

            migrationBuilder.CreateIndex(
                name: "IX_shop_items_kid_id",
                table: "shop_items",
                column: "kid_id");

            migrationBuilder.CreateIndex(
                name: "IX_parent_settings_parent_id",
                table: "parent_settings",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_chore_submissions_approved_by_user_id",
                table: "chore_submissions",
                column: "approved_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chore_submissions_chores_chore_id",
                table: "chore_submissions",
                column: "chore_id",
                principalTable: "chores",
                principalColumn: "chore_id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_parent_settings_parents_parent_id",
                table: "parent_settings",
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

            migrationBuilder.AddForeignKey(
                name: "FK_shop_items_parents_parent_id",
                table: "shop_items",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "parent_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
