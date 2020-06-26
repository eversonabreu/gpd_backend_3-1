using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    public partial class UserDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string password = GPD.Commom.Utils.Cryptography.Encrypt("1234");
            string sql = string.Format("insert into Usuario (Nome, Cpf, Senha, Email, Administrador, Ativo) values ('Admin (default)', '05450395922', '{0}', 'everson.ean@gmail.com', 1, 1);", password);
            migrationBuilder.Sql(sql, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
