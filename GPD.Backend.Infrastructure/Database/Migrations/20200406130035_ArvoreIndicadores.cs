using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class ArvoreIndicadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjetoEstruturaOrganizacional",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProjeto = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    PosicaoEstrutura = table.Column<int>(nullable: false),
                    IdNivelOrganizacional = table.Column<long>(nullable: true),
                    IdUsuario = table.Column<long>(nullable: true),
                    IdIndicador = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkProjetoEstruturaOrganizacional", x => x.Id);
                    table.ForeignKey(
                        name: "FKProjetoEstruturaOrganizacionalIndicador",
                        column: x => x.IdIndicador,
                        principalTable: "Indicador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKProjetoEstruturaOrganizacionalNivelOrganizacional",
                        column: x => x.IdNivelOrganizacional,
                        principalTable: "NivelOrganizacional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKProjetoEstruturaOrganizacionalProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKProjetoEstruturaOrganizacionalUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdIndicador",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdIndicador");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdNivelOrganizacional",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdNivelOrganizacional");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdProjeto",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdUsuario",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoEstruturaOrganizacional");
        }
    }
}
