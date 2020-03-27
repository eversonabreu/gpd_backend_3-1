using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable(nameof(Estado));
            builder.HasKey(bld => bld.Id).HasName("PkEstado");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.CodigoUfIbge).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(EstadoConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Sigla).HasMaxLength(EstadoConsts.TamanhoColunaSigla).IsRequired();
            builder.HasIndex(c => c.Nome).HasName("UkEstadoNome").IsUnique();
            builder.HasIndex(c => c.Sigla).HasName("UkEstadoSigla").IsUnique();
            builder.HasIndex(c => c.CodigoUfIbge).HasName("UkEstadoCodigoUfIbge").IsUnique();
        }
    }
}
