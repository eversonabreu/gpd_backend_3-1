using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class NivelOrganizacionalMapping : IEntityTypeConfiguration<NivelOrganizacional>
    {
        public void Configure(EntityTypeBuilder<NivelOrganizacional> builder)
        {
            builder.ToTable(nameof(NivelOrganizacional));
            builder.HasKey(bld => bld.Id).HasName("PkNivelOrganizacional");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(NivelOrganizacionalConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Descricao).HasMaxLength(NivelOrganizacionalConsts.TamanhoColunaDescricao);
            builder.Property(bld => bld.Tipo).IsRequired();
        }
    }
}
