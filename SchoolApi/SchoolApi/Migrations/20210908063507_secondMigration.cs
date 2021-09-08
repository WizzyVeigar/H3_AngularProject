using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApi.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoResistor",
                table: "PhotoResistor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HumidityTempSensor",
                table: "HumidityTempSensor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoResistor",
                table: "PhotoResistor",
                column: "PhotoResistorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HumidityTempSensor",
                table: "HumidityTempSensor",
                column: "HumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoResistor_Id",
                table: "PhotoResistor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityTempSensor_Id",
                table: "HumidityTempSensor",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoResistor",
                table: "PhotoResistor");

            migrationBuilder.DropIndex(
                name: "IX_PhotoResistor_Id",
                table: "PhotoResistor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HumidityTempSensor",
                table: "HumidityTempSensor");

            migrationBuilder.DropIndex(
                name: "IX_HumidityTempSensor_Id",
                table: "HumidityTempSensor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoResistor",
                table: "PhotoResistor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HumidityTempSensor",
                table: "HumidityTempSensor",
                column: "Id");
        }
    }
}
