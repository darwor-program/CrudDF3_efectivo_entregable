using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDF3.Migrations
{
    /// <inheritdoc />
    public partial class lol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ReservasP__IdPaq__656C112C",
                table: "ReservasPaquetes");

            migrationBuilder.AlterColumn<int>(
                name: "IdPaquete",
                table: "ReservasPaquetes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__ReservasP__IdPaq__656C112C",
                table: "ReservasPaquetes",
                column: "IdPaquete",
                principalTable: "PaquetesTuristicos",
                principalColumn: "IdPaquete",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ReservasP__IdPaq__656C112C",
                table: "ReservasPaquetes");

            migrationBuilder.AlterColumn<int>(
                name: "IdPaquete",
                table: "ReservasPaquetes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__ReservasP__IdPaq__656C112C",
                table: "ReservasPaquetes",
                column: "IdPaquete",
                principalTable: "PaquetesTuristicos",
                principalColumn: "IdPaquete");
        }
    }
}
