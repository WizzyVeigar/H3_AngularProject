using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApi.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_Id",
                table: "HumidityTempSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoResistor_DataEntry_Id",
                table: "PhotoResistor");

            migrationBuilder.DropIndex(
                name: "IX_PhotoResistor_Id",
                table: "PhotoResistor");

            migrationBuilder.DropIndex(
                name: "IX_HumidityTempSensor_Id",
                table: "HumidityTempSensor");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoResistorId",
                table: "PhotoResistor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "HumId",
                table: "HumidityTempSensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_HumId",
                table: "HumidityTempSensor",
                column: "HumId",
                principalTable: "DataEntry",
                principalColumn: "HumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoResistor_DataEntry_PhotoResistorId",
                table: "PhotoResistor",
                column: "PhotoResistorId",
                principalTable: "DataEntry",
                principalColumn: "PhotoResistorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_HumId",
                table: "HumidityTempSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoResistor_DataEntry_PhotoResistorId",
                table: "PhotoResistor");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoResistorId",
                table: "PhotoResistor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "HumId",
                table: "HumidityTempSensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoResistor_Id",
                table: "PhotoResistor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityTempSensor_Id",
                table: "HumidityTempSensor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_Id",
                table: "HumidityTempSensor",
                column: "Id",
                principalTable: "DataEntry",
                principalColumn: "HumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoResistor_DataEntry_Id",
                table: "PhotoResistor",
                column: "Id",
                principalTable: "DataEntry",
                principalColumn: "PhotoResistorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
