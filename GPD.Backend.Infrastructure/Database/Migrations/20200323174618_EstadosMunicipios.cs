using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class EstadosMunicipios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoUfIbge = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Sigla = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoMunicipioIbge = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkMunicipio", x => x.Id);
                    table.ForeignKey(
                        name: "FKMunicipioEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UkEstadoCodigoUfIbge",
                table: "Estado",
                column: "CodigoUfIbge",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UkEstadoNome",
                table: "Estado",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UkEstadoSigla",
                table: "Estado",
                column: "Sigla",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UkMunicipioCodigoMunicipioIbge",
                table: "Municipio",
                column: "CodigoMunicipioIbge",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_IdEstado",
                table: "Municipio",
                column: "IdEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
