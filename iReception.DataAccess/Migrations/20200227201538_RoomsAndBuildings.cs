using Microsoft.EntityFrameworkCore.Migrations;

namespace iReception.DataAccess.Migrations
{
    public partial class RoomsAndBuildings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDamaged",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FurnitureValue",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinuteServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PricePerMinute = table.Column<decimal>(nullable: false),
                    MinTime = table.Column<int>(nullable: false),
                    MaxTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinuteServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomToMinuteServices",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    MinuteServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomToMinuteServices", x => new { x.RoomId, x.MinuteServiceId });
                    table.ForeignKey(
                        name: "FK_RoomToMinuteServices_MinuteServices_MinuteServiceId",
                        column: x => x.MinuteServiceId,
                        principalTable: "MinuteServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomToMinuteServices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomToMinuteServices_MinuteServiceId",
                table: "RoomToMinuteServices",
                column: "MinuteServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Buildings_BuildingId",
                table: "Rooms",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Buildings_BuildingId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "RoomToMinuteServices");

            migrationBuilder.DropTable(
                name: "MinuteServices");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsDamaged",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "FurnitureValue",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
