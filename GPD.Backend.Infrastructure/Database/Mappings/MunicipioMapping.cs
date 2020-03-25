using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class MunicipioMapping : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable(MunicipioConsts.NomeTabela);
            builder.HasKey(bld => bld.Id).HasName("PkMunicipio");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(MunicipioConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.IdEstado).IsRequired();
            builder.Property(bld => bld.CodigoMunicipioIbge).IsRequired();
            builder.HasIndex(c => c.CodigoMunicipioIbge).HasName("UkMunicipioCodigoMunicipioIbge").IsUnique();
            builder.HasOne(bld => bld.Estado)
                 .WithMany(bld => bld.Municipios)
                 .HasForeignKey(bld => bld.IdEstado)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKMunicipioEstado")
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
