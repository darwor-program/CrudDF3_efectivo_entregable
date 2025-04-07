using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDF3.Migrations
{
    /// <inheritdoc />
    public partial class PrimerCambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoHabitacion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CapacidadHuespedes = table.Column<int>(type: "int", nullable: true),
                    EstadoHabitacion = table.Column<bool>(type: "bit", nullable: false),
                    DescripcionHabitacion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TarifaHabitacion = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CaracteristicasHabitacion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habitaci__8BBBF901646793AD", x => x.IdHabitacion);
                });

            migrationBuilder.CreateTable(
                name: "Huespedes",
                columns: table => new
                {
                    IdHuesped = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedulaHuesped = table.Column<int>(type: "int", nullable: true),
                    NombreHuesped = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ApellidoHuesped = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CorreoHuesped = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FechaEntradaHuesped = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaSalidaHuesped = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Huespede__939EC0618E8E6C9A", x => x.IdHuesped);
                });

            migrationBuilder.CreateTable(
                name: "PaquetesTuristicos",
                columns: table => new
                {
                    IdPaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePaquete = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DescripcionPaquete = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PrecioPaquete = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DisponibilidadPaquete = table.Column<bool>(type: "bit", nullable: false),
                    FechaPaquete = table.Column<DateTime>(type: "datetime", nullable: true),
                    DestinoPaquete = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EstadoPaquete = table.Column<bool>(type: "bit", nullable: false),
                    TipoViajePaquete = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paquetes__DE278F8B44976DDC", x => x.IdPaquete);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePermiso = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DescripcionPermiso = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    EstadoPermiso = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__0D626EC89D1425DC", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DescripcionRol = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    EstadoRol = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__2A49584C1BB668CC", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreServicio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Categoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Disponibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EstadoServicio = table.Column<bool>(type: "bit", nullable: false),
                    TipoServicio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Servicio__2DCCF9A2E3474427", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteHabitaciones",
                columns: table => new
                {
                    IdPaquete = table.Column<int>(type: "int", nullable: false),
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteHabitaciones", x => new { x.IdPaquete, x.IdHabitacion });
                    table.ForeignKey(
                        name: "FK_PaqueteHabitaciones_Habitaciones_IdHabitacion",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitaciones",
                        principalColumn: "IdHabitacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteHabitaciones_PaquetesTuristicos_IdPaquete",
                        column: x => x.IdPaquete,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "IdPaquete",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rol_Permisos",
                columns: table => new
                {
                    IdRolPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: true),
                    IdPermiso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol_Perm__0CC73B1B717C1734", x => x.IdRolPermiso);
                    table.ForeignKey(
                        name: "FK__Rol_Permi__IdPer__5DCAEF64",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                    table.ForeignKey(
                        name: "FK__Rol_Permi__IdRol__5CD6CB2B",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedulaUsuario = table.Column<int>(type: "int", nullable: true),
                    NombreUsuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ApellidoUsuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CorreoUsuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ContraseñaUsuario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EstadoUsuario = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__5B65BF97118D37CE", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuarios__IdRol__4D94879B",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "PaqueteServicios",
                columns: table => new
                {
                    IdPaquete = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteServicios", x => new { x.IdPaquete, x.IdServicio });
                    table.ForeignKey(
                        name: "FK_PaqueteServicios_PaquetesTuristicos_IdPaquete",
                        column: x => x.IdPaquete,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "IdPaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteServicios_Servicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaInicial = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFinal = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Anticipo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FechaReserva = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoReserva = table.Column<bool>(type: "bit", nullable: false),
                    HuespedeIdHuesped = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__0E49C69DE86ED4F0", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Huespedes_HuespedeIdHuesped",
                        column: x => x.HuespedeIdHuesped,
                        principalTable: "Huespedes",
                        principalColumn: "IdHuesped");
                    table.ForeignKey(
                        name: "FK__Reservas__IdUsua__5812160E",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ReservasPaquetes",
                columns: table => new
                {
                    IdReservaPaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReserva = table.Column<int>(type: "int", nullable: true),
                    IdPaquete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__BF8B0A63C143DE0D", x => x.IdReservaPaquete);
                    table.ForeignKey(
                        name: "FK__ReservasP__IdPaq__656C112C",
                        column: x => x.IdPaquete,
                        principalTable: "PaquetesTuristicos",
                        principalColumn: "IdPaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ReservasP__IdRes__6477ECF3",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteHabitaciones_IdHabitacion",
                table: "PaqueteHabitaciones",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicios_IdServicio",
                table: "PaqueteServicios",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HuespedeIdHuesped",
                table: "Reservas",
                column: "HuespedeIdHuesped");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdUsuario",
                table: "Reservas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasPaquetes_IdPaquete",
                table: "ReservasPaquetes",
                column: "IdPaquete");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasPaquetes_IdReserva",
                table: "ReservasPaquetes",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Permisos_IdPermiso",
                table: "Rol_Permisos",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Permisos_IdRol",
                table: "Rol_Permisos",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaqueteHabitaciones");

            migrationBuilder.DropTable(
                name: "PaqueteServicios");

            migrationBuilder.DropTable(
                name: "ReservasPaquetes");

            migrationBuilder.DropTable(
                name: "Rol_Permisos");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "PaquetesTuristicos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Huespedes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
