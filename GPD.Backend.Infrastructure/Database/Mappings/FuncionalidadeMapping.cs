using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class FuncionalidadeMapping : IEntityTypeConfiguration<Funcionalidade>
    {
        public void Configure(EntityTypeBuilder<Funcionalidade> builder)
        {
            builder.ToTable(FuncionalidadeConsts.NomeTabela);
            builder.HasKey(bld => bld.Id).HasName("PkFuncionalidade");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(FuncionalidadeConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Controlador).HasMaxLength(FuncionalidadeConsts.TamanhoColunaControlador).IsRequired();
            builder.HasIndex(bld => bld.Controlador).HasName("UkFuncionalidade").IsUnique();
        }
    }
}
