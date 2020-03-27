using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class IndicadorMapping : IEntityTypeConfiguration<Indicador>
    {
        public void Configure(EntityTypeBuilder<Indicador> builder)
        {
            builder.ToTable(nameof(Indicador));
            builder.HasKey(bld => bld.Id).HasName("PkIndicador");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(IndicadorConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Ativo).IsRequired();
            builder.Property(bld => bld.Ordem);
            builder.Property(bld => bld.ValorPercentualPeso).HasColumnType("decimal(3, 2)").IsRequired();
            builder.Property(bld => bld.ValorPercentualCriterio).HasColumnType("decimal(3, 2)").IsRequired();
            builder.Property(bld => bld.TipoRemuneracao).IsRequired();
            builder.Property(bld => bld.TipoCardinalidade).IsRequired();
            builder.Property(bld => bld.TipoPeriodicidade).IsRequired();
            builder.Property(bld => bld.TipoAcumuloMeta).IsRequired();
            builder.Property(bld => bld.TipoAcumuloRealizado).IsRequired();
            builder.Property(bld => bld.IdUnidadeMedida).IsRequired();
            builder.Property(bld => bld.Corporativo).IsRequired();
            builder.Property(bld => bld.IdUsuarioResponsavel);
            builder.Property(bld => bld.Formula);
            builder.Property(bld => bld.Observacao);
            builder.Property(bld => bld.ValorMinimoAtingimento).HasColumnType("decimal(20, 2)");
            builder.Property(bld => bld.ValorMaximoAtingimento).HasColumnType("decimal(20, 2)");
            builder.Property(bld => bld.ValorMinimoPonderado).HasColumnType("decimal(20, 2)");
            builder.Property(bld => bld.ValorMaximoPonderado).HasColumnType("decimal(20, 2)");

            builder.HasOne(bld => bld.UsuarioResponsavel)
                 .WithMany(bld => bld.Indicadores)
                 .HasForeignKey(bld => bld.IdUsuarioResponsavel)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKIndicadorUsuarioResponsavel")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.UnidadeMedida)
                 .WithMany(bld => bld.Indicadores)
                 .HasForeignKey(bld => bld.IdUnidadeMedida)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKIndicadorUnidadeMedida")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bld => bld.Identificador).HasName("UkIndicador").IsUnique();
        }
    }
}
