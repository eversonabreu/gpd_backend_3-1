using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class OrdemArvore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Indicador");

            migrationBuilder.AddColumn<short>(
                name: "Ordem",
                table: "ProjetoEstruturaOrganizacional",
                nullable: false,
                defaultValue: (short)1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "ProjetoEstruturaOrganizacional");

            migrationBuilder.AddColumn<short>(
                name: "Ordem",
                table: "Indicador",
                type: "smallint",
                nullable: true);
        }
    }
}
