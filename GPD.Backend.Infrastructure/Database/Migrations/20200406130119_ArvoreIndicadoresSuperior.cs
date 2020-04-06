using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class ArvoreIndicadoresSuperior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdSuperior",
                table: "ProjetoEstruturaOrganizacional",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdSuperior",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdSuperior");

            migrationBuilder.AddForeignKey(
                name: "FKProjetoEstruturaOrganizacionalSuperior",
                table: "ProjetoEstruturaOrganizacional",
                column: "IdSuperior",
                principalTable: "ProjetoEstruturaOrganizacional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKProjetoEstruturaOrganizacionalSuperior",
                table: "ProjetoEstruturaOrganizacional");

            migrationBuilder.DropIndex(
                name: "IX_ProjetoEstruturaOrganizacional_IdSuperior",
                table: "ProjetoEstruturaOrganizacional");

            migrationBuilder.DropColumn(
                name: "IdSuperior",
                table: "ProjetoEstruturaOrganizacional");
        }
    }
}
