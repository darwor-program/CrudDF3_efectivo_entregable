using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDF3.Migrations
{
    /// <inheritdoc />
    public partial class SegundoCambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Reservas__IdHabi__59FA5E80",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK__Reservas__IdHues__59063A47",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "ReservasServicios");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_IdHabitacion",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "IdHabitacion",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "IdHuesped",
                table: "Reservas",
                newName: "HuespedeIdHuesped");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_IdHuesped",
                table: "Reservas",
                newName: "IX_Reservas_HuespedeIdHuesped");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Huespedes_HuespedeIdHuesped",
                table: "Reservas",
                column: "HuespedeIdHuesped",
                principalTable: "Huespedes",
                principalColumn: "IdHuesped");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Huespedes_HuespedeIdHuesped",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "HuespedeIdHuesped",
                table: "Reservas",
                newName: "IdHuesped");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_HuespedeIdHuesped",
                table: "Reservas",
                newName: "IX_Reservas_IdHuesped");

            migrationBuilder.AddColumn<int>(
                name: "IdHabitacion",
                table: "Reservas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservasServicios",
                columns: table => new
                {
                    IdReservaServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReserva = table.Column<int>(type: "int", nullable: true),
                    IdServicio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__B3FBC747E65BE0F4", x => x.IdReservaServicio);
                    table.ForeignKey(
                        name: "FK__ReservasS__IdRes__60A75C0F",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva");
                    table.ForeignKey(
                        name: "FK__ReservasS__IdSer__619B8048",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdHabitacion",
                table: "Reservas",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasServicios_IdReserva",
                table: "ReservasServicios",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasServicios_IdServicio",
                table: "ReservasServicios",
                column: "IdServicio");

            migrationBuilder.AddForeignKey(
                name: "FK__Reservas__IdHabi__59FA5E80",
                table: "Reservas",
                column: "IdHabitacion",
                principalTable: "Habitaciones",
                principalColumn: "IdHabitacion");

            migrationBuilder.AddForeignKey(
                name: "FK__Reservas__IdHues__59063A47",
                table: "Reservas",
                column: "IdHuesped",
                principalTable: "Huespedes",
                principalColumn: "IdHuesped");
        }
    }
}
