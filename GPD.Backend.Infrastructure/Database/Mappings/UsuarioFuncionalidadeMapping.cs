using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class UsuarioFuncionalidadeMapping : IEntityTypeConfiguration<UsuarioFuncionalidade>
    {
        public void Configure(EntityTypeBuilder<UsuarioFuncionalidade> builder)
        {
            builder.ToTable(nameof(UsuarioFuncionalidade));
            builder.HasKey(bld => bld.Id).HasName("PkUsuarioFuncionalidade");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.IdUsuario).IsRequired();
            builder.Property(bld => bld.IdFuncionalidade).IsRequired();
            builder.Property(bld => bld.PermiteInserir).IsRequired();
            builder.Property(bld => bld.PermiteEditar).IsRequired();
            builder.Property(bld => bld.PermiteExcluir).IsRequired();

            builder.HasOne(bld => bld.Usuario)
                .WithMany(bld => bld.UsuarioFuncionalidades)
                .HasForeignKey(bld => bld.IdUsuario)
                .HasPrincipalKey(bld => bld.Id)
                .HasConstraintName("FKUsuarioFuncionalidadeUsuario")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Funcionalidade)
                .WithMany(bld => bld.UsuariosFuncionalidade)
                .HasForeignKey(bld => bld.IdFuncionalidade)
                .HasPrincipalKey(bld => bld.Id)
                .HasConstraintName("FKUsuarioFuncionalidadePerfil")
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(bld => new { bld.IdUsuario, bld.IdFuncionalidade }).HasName("UkUsuarioFuncionalidade").IsUnique();
        }
    }
}
