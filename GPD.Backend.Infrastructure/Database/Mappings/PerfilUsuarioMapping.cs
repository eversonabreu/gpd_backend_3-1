using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class PerfilUsuarioMapping : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.ToTable(nameof(PerfilUsuario));
            builder.HasKey(bld => bld.Id).HasName("PkPerfilUsuario");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.IdPerfil).IsRequired();
            builder.Property(bld => bld.IdUsuario).IsRequired();

            builder.HasOne(bld => bld.Usuario)
                .WithMany(bld => bld.PerfisUsuario)
                .HasForeignKey(bld => bld.IdUsuario)
                .HasPrincipalKey(bld => bld.Id)
                .HasConstraintName("FKPerfilUsuarioUsuario")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Perfil)
                .WithMany(bld => bld.PerfilUsuarios)
                .HasForeignKey(bld => bld.IdPerfil)
                .HasPrincipalKey(bld => bld.Id)
                .HasConstraintName("FKPerfilUsuarioPerfil")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bld => new { bld.IdPerfil, bld.IdUsuario }).HasName("UkPerfilUsuario").IsUnique();
        }
    }
}
