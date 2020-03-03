using Microsoft.EntityFrameworkCore.Migrations;

namespace iReception.DataAccess.Migrations
{
    public partial class RoomAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Rooms",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Buildings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Buildings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Buildings");
        }
    }
}
