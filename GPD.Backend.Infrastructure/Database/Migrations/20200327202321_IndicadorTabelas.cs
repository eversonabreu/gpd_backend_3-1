using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class IndicadorTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeTabela = table.Column<string>(maxLength: 150, nullable: false),
                    IdRegistro = table.Column<long>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<long>(nullable: false),
                    Objeto = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelOrganizacional",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkNivelOrganizacional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkProjeto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedida",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 255, nullable: false),
                    Sigla = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkUnidadeMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indicador",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Identificador = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Ordem = table.Column<short>(nullable: true),
                    ValorPercentualPeso = table.Column<decimal>(type: "decimal(3, 2)", nullable: false),
                    ValorPercentualCriterio = table.Column<decimal>(type: "decimal(3, 2)", nullable: false),
                    TipoRemuneracao = table.Column<int>(nullable: false),
                    TipoCardinalidade = table.Column<int>(nullable: false),
                    TipoPeriodicidade = table.Column<int>(nullable: false),
                    TipoAcumuloMeta = table.Column<int>(nullable: false),
                    TipoAcumuloRealizado = table.Column<int>(nullable: false),
                    IdUnidadeMedida = table.Column<long>(nullable: false),
                    Corporativo = table.Column<bool>(nullable: false),
                    IdUsuarioResponsavel = table.Column<long>(nullable: true),
                    Formula = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    ValorMinimoAtingimento = table.Column<decimal>(type: "decimal(20, 2)", nullable: true),
                    ValorMaximoAtingimento = table.Column<decimal>(type: "decimal(20, 2)", nullable: true),
                    ValorMinimoPonderado = table.Column<decimal>(type: "decimal(20, 2)", nullable: true),
                    ValorMaximoPonderado = table.Column<decimal>(type: "decimal(20, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkIndicador", x => x.Id);
                    table.ForeignKey(
                        name: "FKIndicadorUnidadeMedida",
                        column: x => x.IdUnidadeMedida,
                        principalTable: "UnidadeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKIndicadorUsuarioResponsavel",
                        column: x => x.IdUsuarioResponsavel,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndicadorLancamento",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProjeto = table.Column<long>(nullable: false),
                    IdIndicador = table.Column<long>(nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "date", nullable: false),
                    ValorMeta = table.Column<decimal>(type: "decimal(20, 2)", nullable: false),
                    ValorRealizado = table.Column<decimal>(type: "decimal(20, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkIndicadorLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FKIndicadorLancamentoIndicador",
                        column: x => x.IdIndicador,
                        principalTable: "Indicador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKIndicadorLancamentoProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Indicador_IdUnidadeMedida",
                table: "Indicador",
                column: "IdUnidadeMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Indicador_IdUsuarioResponsavel",
                table: "Indicador",
                column: "IdUsuarioResponsavel");

            migrationBuilder.CreateIndex(
                name: "UkIndicador",
                table: "Indicador",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndicadorLancamento_IdIndicador",
                table: "IndicadorLancamento",
                column: "IdIndicador");

            migrationBuilder.CreateIndex(
                name: "UkIndicadorLancamento",
                table: "IndicadorLancamento",
                columns: new[] { "IdProjeto", "IdIndicador", "DataLancamento" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "IndicadorLancamento");

            migrationBuilder.DropTable(
                name: "NivelOrganizacional");

            migrationBuilder.DropTable(
                name: "Indicador");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "UnidadeMedida");
        }
    }
}
