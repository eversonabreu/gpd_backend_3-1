using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class CalculosIndicador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UkIndicadorLancamento",
                table: "IndicadorLancamento");

            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "IndicadorLancamento");

            migrationBuilder.DropColumn(
                name: "TipoAcumuloMeta",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "TipoAcumuloRealizado",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "TipoCardinalidade",
                table: "Indicador");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "IndicadorLancamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "IndicadorLancamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AcumulaMeta",
                table: "Indicador",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcumulaRealizado",
                table: "Indicador",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PossuiCardinalidade",
                table: "Indicador",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TipoCalculo",
                table: "Indicador",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UkIndicadorLancamentoPro",
                table: "IndicadorLancamento",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "UkIndicadorLancamentoProIn",
                table: "IndicadorLancamento",
                columns: new[] { "IdProjeto", "IdIndicador" });

            migrationBuilder.CreateIndex(
                name: "UkIndicadorLancamento",
                table: "IndicadorLancamento",
                columns: new[] { "IdProjeto", "IdIndicador", "Mes", "Ano" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_NomeTabela_IdRegistro",
                table: "Auditoria",
                columns: new[] { "NomeTabela", "IdRegistro" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UkIndicadorLancamentoPro",
                table: "IndicadorLancamento");

            migrationBuilder.DropIndex(
                name: "UkIndicadorLancamentoProIn",
                table: "IndicadorLancamento");

            migrationBuilder.DropIndex(
                name: "UkIndicadorLancamento",
                table: "IndicadorLancamento");

            migrationBuilder.DropIndex(
                name: "IX_Auditoria_NomeTabela_IdRegistro",
                table: "Auditoria");

            migrationBuilder.DropColumn(
                name: "Ano",
                table: "IndicadorLancamento");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "IndicadorLancamento");

            migrationBuilder.DropColumn(
                name: "AcumulaMeta",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "AcumulaRealizado",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "PossuiCardinalidade",
                table: "Indicador");

            migrationBuilder.DropColumn(
                name: "TipoCalculo",
                table: "Indicador");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "IndicadorLancamento",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TipoAcumuloMeta",
                table: "Indicador",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoAcumuloRealizado",
                table: "Indicador",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoCardinalidade",
                table: "Indicador",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UkIndicadorLancamento",
                table: "IndicadorLancamento",
                columns: new[] { "IdProjeto", "IdIndicador", "DataLancamento" },
                unique: true);
        }
    }
}
