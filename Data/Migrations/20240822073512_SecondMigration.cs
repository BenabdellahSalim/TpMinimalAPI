using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpMinimalAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Todo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 22, 9, 35, 12, 123, DateTimeKind.Local).AddTicks(5772),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Todo",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 8, 22, 9, 35, 12, 123, DateTimeKind.Local).AddTicks(5772));
        }
    }
}
