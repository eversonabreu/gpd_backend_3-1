using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable(PerfilConsts.NomeTabela);
            builder.HasKey(bld => bld.Id).HasName("PkPerfil");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(PerfilConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Descricao).HasMaxLength(PerfilConsts.TamanhoColunaDescricao).IsRequired();
            builder.Property(bld => bld.Codigo).IsRequired();
            builder.HasIndex(bld => bld.Codigo).HasName("UkPerfil").IsUnique();
        }
    }
}
