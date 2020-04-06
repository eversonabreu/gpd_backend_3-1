using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class NovosCalculosIndicador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorMaximoAtingimento",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "ValorMaximoPonderado",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "ValorMinimoAtingimento",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "ValorMinimoPonderado",
                table: "Indicador");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPesoCorporativo",
                table: "Usuario",
                type: "decimal(20,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPesoIndividual",
                table: "Usuario",
                type: "decimal(20,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorPesoCorporativo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ValorPesoIndividual",
                table: "Usuario");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMaximoAtingimento",
                table: "Indicador",
                type: "decimal(20, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMaximoPonderado",
                table: "Indicador",
                type: "decimal(20, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMinimoAtingimento",
                table: "Indicador",
                type: "decimal(20, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMinimoPonderado",
                table: "Indicador",
                type: "decimal(20, 2)",
                nullable: true);
        }
    }
}
