using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));
            builder.HasKey(bld => bld.Id).HasName("PkUsuario");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(UsuarioConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Login).HasMaxLength(UsuarioConsts.TamanhoColunaLogin).IsRequired();
            builder.Property(bld => bld.Senha).HasColumnType("text").IsRequired();
            builder.Property(bld => bld.Email).HasMaxLength(UsuarioConsts.TamanhoColunaEmail).IsRequired();
            builder.Property(bld => bld.Administrador).IsRequired();
            builder.Property(bld => bld.Ativo).IsRequired();
            builder.HasIndex(bld => bld.Login).HasName("UkUsuario").IsUnique();
        }
    }
}
