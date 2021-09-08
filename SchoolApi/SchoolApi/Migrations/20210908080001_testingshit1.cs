using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApi.Migrations
{
    public partial class testingshit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HumidityTempSensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    TimeOccured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityTempSensor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoResistor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LightLevel = table.Column<int>(type: "int", nullable: false),
                    TimeOccured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoResistor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataEntry",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HumidId = table.Column<int>(type: "int", nullable: false),
                    PhotoResId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataEntry", x => new { x.RoomNumber, x.CreatedTime });
                    table.ForeignKey(
                        name: "FK_DataEntry_HumidityTempSensor_HumidId",
                        column: x => x.HumidId,
                        principalTable: "HumidityTempSensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataEntry_PhotoResistor_PhotoResId",
                        column: x => x.PhotoResId,
                        principalTable: "PhotoResistor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataEntry_HumidId",
                table: "DataEntry",
                column: "HumidId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEntry_PhotoResId",
                table: "DataEntry",
                column: "PhotoResId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataEntry");

            migrationBuilder.DropTable(
                name: "HumidityTempSensor");

            migrationBuilder.DropTable(
                name: "PhotoResistor");
        }
    }
}
