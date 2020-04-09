using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class ImportacaoLancamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "ProjetoEstruturaOrganizacional");

            migrationBuilder.DropColumn(
                name: "PosicaoEstrutura",
                table: "ProjetoEstruturaOrganizacional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "ProjetoEstruturaOrganizacional",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PosicaoEstrutura",
                table: "ProjetoEstruturaOrganizacional",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
