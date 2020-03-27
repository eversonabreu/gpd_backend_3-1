using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class AuditoriaMapping : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable(nameof(Auditoria));
            builder.HasKey(bld => bld.Id).HasName("PkAuditoria");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.NomeTabela).HasMaxLength(150).IsRequired();
            builder.Property(bld => bld.IdRegistro).IsRequired();
            builder.Property(bld => bld.DataRegistro).IsRequired();
            builder.Property(bld => bld.Tipo).IsRequired();
            builder.Property(bld => bld.IdUsuario).IsRequired();
            builder.Property(bld => bld.Objeto).HasColumnType("text");
        }
    }
}
