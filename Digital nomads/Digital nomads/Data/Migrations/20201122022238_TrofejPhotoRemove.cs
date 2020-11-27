using Microsoft.EntityFrameworkCore.Migrations;

namespace Digital_nomads.Data.Migrations
{
    public partial class TrofejPhotoRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoRuta",
                table: "Trofej");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoRuta",
                table: "Trofej",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
