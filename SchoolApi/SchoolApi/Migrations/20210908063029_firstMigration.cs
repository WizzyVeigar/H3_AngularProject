using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApi.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataEntry",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HumId = table.Column<int>(type: "int", nullable: false),
                    PhotoResistorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataEntry", x => new { x.RoomNumber, x.CreatedTime });
                    table.UniqueConstraint("AK_DataEntry_HumId", x => x.HumId);
                    table.UniqueConstraint("AK_DataEntry_PhotoResistorId", x => x.PhotoResistorId);
                });

            migrationBuilder.CreateTable(
                name: "HumidityTempSensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    DataEntryCreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataEntryRoomNumber = table.Column<int>(type: "int", nullable: true),
                    HumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOccured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityTempSensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumidityTempSensor_DataEntry_DataEntryRoomNumber_DataEntryCreatedTime",
                        columns: x => new { x.DataEntryRoomNumber, x.DataEntryCreatedTime },
                        principalTable: "DataEntry",
                        principalColumns: new[] { "RoomNumber", "CreatedTime" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HumidityTempSensor_DataEntry_Id",
                        column: x => x.Id,
                        principalTable: "DataEntry",
                        principalColumn: "HumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoResistor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LightLevel = table.Column<int>(type: "int", nullable: false),
                    DataEntryCreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataEntryRoomNumber = table.Column<int>(type: "int", nullable: true),
                    PhotoResistorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOccured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoResistor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoResistor_DataEntry_DataEntryRoomNumber_DataEntryCreatedTime",
                        columns: x => new { x.DataEntryRoomNumber, x.DataEntryCreatedTime },
                        principalTable: "DataEntry",
                        principalColumns: new[] { "RoomNumber", "CreatedTime" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoResistor_DataEntry_Id",
                        column: x => x.Id,
                        principalTable: "DataEntry",
                        principalColumn: "PhotoResistorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HumidityTempSensor_DataEntryRoomNumber_DataEntryCreatedTime",
                table: "HumidityTempSensor",
                columns: new[] { "DataEntryRoomNumber", "DataEntryCreatedTime" });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoResistor_DataEntryRoomNumber_DataEntryCreatedTime",
                table: "PhotoResistor",
                columns: new[] { "DataEntryRoomNumber", "DataEntryCreatedTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumidityTempSensor");

            migrationBuilder.DropTable(
                name: "PhotoResistor");

            migrationBuilder.DropTable(
                name: "DataEntry");
        }
    }
}
