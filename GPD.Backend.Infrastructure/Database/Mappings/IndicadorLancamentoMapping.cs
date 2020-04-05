using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class IndicadorLancamentoMapping : IEntityTypeConfiguration<IndicadorLancamento>
    {
        public void Configure(EntityTypeBuilder<IndicadorLancamento> builder)
        {
            builder.ToTable(nameof(IndicadorLancamento));
            builder.HasKey(bld => bld.Id).HasName("PkIndicadorLancamento");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.IdProjeto).IsRequired();
            builder.Property(bld => bld.IdIndicador).IsRequired();
            builder.Property(bld => bld.Mes).IsRequired();
            builder.Property(bld => bld.Ano).IsRequired();
            builder.Property(bld => bld.ValorMeta).HasColumnType("decimal(20, 2)").IsRequired();
            builder.Property(bld => bld.ValorRealizado).HasColumnType("decimal(20, 2)").IsRequired();

            builder.HasOne(bld => bld.Projeto)
                 .WithMany(bld => bld.IndicadorLancamentos)
                 .HasForeignKey(bld => bld.IdProjeto)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKIndicadorLancamentoProjeto")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Indicador)
                 .WithMany(bld => bld.IndicadorLancamentos)
                 .HasForeignKey(bld => bld.IdIndicador)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKIndicadorLancamentoIndicador")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bld => new { bld.IdProjeto }).HasName("UkIndicadorLancamentoPro");
            builder.HasIndex(bld => new { bld.IdProjeto, bld.IdIndicador}).HasName("UkIndicadorLancamentoProIn");
            builder.HasIndex(bld => new { bld.IdProjeto, bld.IdIndicador, bld.Mes, bld.Ano}).HasName("UkIndicadorLancamento").IsUnique();
        }
    }
}
