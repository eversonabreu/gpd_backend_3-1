using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class ControleAcesso_n1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionalidade",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Controlador = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkFuncionalidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkPerfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Login = table.Column<string>(maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Administrador = table.Column<bool>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPerfil = table.Column<long>(nullable: false),
                    IdUsuario = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkPerfilUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FKPerfilUsuarioPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKPerfilUsuarioUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioFuncionalidade",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<long>(nullable: false),
                    IdFuncionalidade = table.Column<long>(nullable: false),
                    PermiteInserir = table.Column<bool>(nullable: false),
                    PermiteEditar = table.Column<bool>(nullable: false),
                    PermiteExcluir = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkUsuarioFuncionalidade", x => x.Id);
                    table.ForeignKey(
                        name: "FKUsuarioFuncionalidadePerfil",
                        column: x => x.IdFuncionalidade,
                        principalTable: "Funcionalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKUsuarioFuncionalidadeUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UkFuncionalidade",
                table: "Funcionalidade",
                column: "Controlador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuario_IdUsuario",
                table: "PerfilUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UkPerfilUsuario",
                table: "PerfilUsuario",
                columns: new[] { "IdPerfil", "IdUsuario" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UkUsuario",
                table: "Usuario",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFuncionalidade_IdFuncionalidade",
                table: "UsuarioFuncionalidade",
                column: "IdFuncionalidade");

            migrationBuilder.CreateIndex(
                name: "UkUsuarioFuncionalidade",
                table: "UsuarioFuncionalidade",
                columns: new[] { "IdUsuario", "IdFuncionalidade" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilUsuario");

            migrationBuilder.DropTable(
                name: "UsuarioFuncionalidade");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Funcionalidade");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
