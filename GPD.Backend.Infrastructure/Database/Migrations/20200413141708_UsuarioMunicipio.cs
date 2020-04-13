using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class UsuarioMunicipio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UkUsuario",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Indicador");

            migrationBuilder.RenameIndex(
                name: "UkMunicipioCodigoMunicipioIbge",
                table: "Municipio",
                newName: "UkMunicipio");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Usuario",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoBairro",
                table: "Usuario",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCep",
                table: "Usuario",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoComplemento",
                table: "Usuario",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoLogradouro",
                table: "Usuario",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoNumero",
                table: "Usuario",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdMunicipio",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Usuario",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UkUsuario",
                table: "Usuario",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdMunicipio",
                table: "Usuario",
                column: "IdMunicipio");

            migrationBuilder.AddForeignKey(
                name: "FKUsuarioMunicipio",
                table: "Usuario",
                column: "IdMunicipio",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKUsuarioMunicipio",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "UkUsuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdMunicipio",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EnderecoBairro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EnderecoCep",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EnderecoComplemento",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EnderecoLogradouro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EnderecoNumero",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdMunicipio",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuario");

            migrationBuilder.RenameIndex(
                name: "UkMunicipio",
                table: "Municipio",
                newName: "UkMunicipioCodigoMunicipioIbge");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Usuario",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Indicador",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "UkUsuario",
                table: "Usuario",
                column: "Login",
                unique: true);
        }
    }
}
