using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class UkPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Perfil",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "UkPerfil",
                table: "Perfil",
                column: "Codigo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UkPerfil",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Perfil");
        }
    }
}
