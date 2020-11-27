using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Digital_nomads.Data.Migrations
{
    public partial class izmjena1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Vjestina",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Kraj",
                table: "Task",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Vjestina");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Task");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Kraj",
                table: "Task",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
