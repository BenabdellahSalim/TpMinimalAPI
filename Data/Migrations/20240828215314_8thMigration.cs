using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpMinimalAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class _8thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Todo",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 8, 22, 9, 35, 12, 123, DateTimeKind.Local).AddTicks(5772));

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Todo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UsersId",
                table: "Todo",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_Users_UsersId",
                table: "Todo",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_Users_UsersId",
                table: "Todo");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Todo_UsersId",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Todo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Todo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 22, 9, 35, 12, 123, DateTimeKind.Local).AddTicks(5772),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
