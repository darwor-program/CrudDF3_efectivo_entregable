using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudDF3.Migrations
{
    /// <inheritdoc />
    public partial class UltimoCambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockPaquete",
                table: "PaquetesTuristicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockPaquete",
                table: "PaquetesTuristicos");
        }
    }
}
