using Microsoft.EntityFrameworkCore.Migrations;

namespace iReception.DataAccess.Migrations
{
    public partial class ManyToManyRelationsTweak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomToServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomToMinuteServices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomToServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomToMinuteServices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
