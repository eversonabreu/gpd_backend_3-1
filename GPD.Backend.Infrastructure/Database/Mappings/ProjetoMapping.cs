using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class ProjetoMapping : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.ToTable(nameof(Projeto));
            builder.HasKey(bld => bld.Id).HasName("PkProjeto");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(ProjetoConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Descricao).HasMaxLength(ProjetoConsts.TamanhoColunaDescricao);
            builder.Property(bld => bld.Ativo).IsRequired();
            builder.Property(bld => bld.DataInicio).HasColumnType("date").IsRequired();
            builder.Property(bld => bld.DataTermino).HasColumnType("date").IsRequired();
        }
    }
}
