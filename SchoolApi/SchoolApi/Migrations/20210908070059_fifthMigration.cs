using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApi.Migrations
{
    public partial class fifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_HumId",
                table: "HumidityTempSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoResistor_DataEntry_PhotoResistorId",
                table: "PhotoResistor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PhotoResistor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HumidityTempSensor");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoResistorId",
                table: "PhotoResistor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PhotoResistorId1",
                table: "PhotoResistor",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HumId",
                table: "HumidityTempSensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "HumId1",
                table: "HumidityTempSensor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoResistor_PhotoResistorId1",
                table: "PhotoResistor",
                column: "PhotoResistorId1");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityTempSensor_HumId1",
                table: "HumidityTempSensor",
                column: "HumId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_HumId1",
                table: "HumidityTempSensor",
                column: "HumId1",
                principalTable: "DataEntry",
                principalColumn: "HumId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoResistor_DataEntry_PhotoResistorId1",
                table: "PhotoResistor",
                column: "PhotoResistorId1",
                principalTable: "DataEntry",
                principalColumn: "PhotoResistorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityTempSensor_DataEntry_HumId1",
                table: "HumidityTempSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoResistor_DataEntry_PhotoResistorId1",
                table: "PhotoResistor");

            migrationBuilder.DropIndex(
                name: "IX_PhotoResistor_PhotoResistorId1",
                table: "PhotoResistor");

            migrationBuilder.DropIndex(
                name: "IX_HumidityTempSensor_HumId1",
                table: "HumidityTempSensor");

            migrationBuilder.DropColumn(
                name: "PhotoResistorId1",
                table: "PhotoResistor");

            migrationBuilder.DropColumn(
                name: "HumId1",
                table: "HumidityTempSensor");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoResistorId",
                table: "PhotoResistor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PhotoResistor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HumId",
                table: "HumidityTempSensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HumidityTempSensor",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
