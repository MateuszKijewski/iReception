using Microsoft.EntityFrameworkCore.Migrations;

namespace iReception.DataAccess.Migrations
{
    public partial class Addedserviceentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Services");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomToMinuteServices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MinuteServices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RoomToServices",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomToServices", x => new { x.RoomId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_RoomToServices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomToServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomToServices_ServiceId",
                table: "RoomToServices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomToServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomToMinuteServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MinuteServices");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
