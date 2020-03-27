using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class UnidadeMedidaMapping : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.ToTable(nameof(UnidadeMedida));
            builder.HasKey(bld => bld.Id).HasName("PkUnidadeMedida");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Descricao).HasMaxLength(UnidadeMedidaConsts.TamanhoColunaDescricao).IsRequired();
            builder.Property(bld => bld.Sigla).HasMaxLength(UnidadeMedidaConsts.TamanhoColunaSigla).IsRequired();
        }
    }
}
