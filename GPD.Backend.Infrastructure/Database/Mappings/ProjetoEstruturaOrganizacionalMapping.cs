using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class ProjetoEstruturaOrganizacionalMapping : IEntityTypeConfiguration<ProjetoEstruturaOrganizacional>
    {
        public void Configure(EntityTypeBuilder<ProjetoEstruturaOrganizacional> builder)
        {
            builder.ToTable(nameof(ProjetoEstruturaOrganizacional));
            builder.HasKey(bld => bld.Id).HasName("PkProjetoEstruturaOrganizacional");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.IdProjeto).IsRequired();
            builder.Property(bld => bld.Tipo).IsRequired();
            builder.Property(bld => bld.Ordem).IsRequired().HasDefaultValue(1);
            builder.Property(bld => bld.IdNivelOrganizacional);
            builder.Property(bld => bld.IdUsuario);
            builder.Property(bld => bld.IdIndicador);
            builder.Property(bld => bld.IdSuperior);


            builder.HasOne(bld => bld.Projeto)
                 .WithMany(bld => bld.ProjetoEstruturasOrganizacionais)
                 .HasForeignKey(bld => bld.IdProjeto)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKProjetoEstruturaOrganizacionalProjeto")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.NivelOrganizacional)
                 .WithMany(bld => bld.ProjetoEstruturasOrganizacionais)
                 .HasForeignKey(bld => bld.IdNivelOrganizacional)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKProjetoEstruturaOrganizacionalNivelOrganizacional")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Usuario)
                 .WithMany(bld => bld.ProjetoEstruturasOrganizacionais)
                 .HasForeignKey(bld => bld.IdUsuario)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKProjetoEstruturaOrganizacionalUsuario")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Indicador)
                 .WithMany(bld => bld.ProjetoEstruturasOrganizacionais)
                 .HasForeignKey(bld => bld.IdIndicador)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKProjetoEstruturaOrganizacionalIndicador")
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bld => bld.Superior)
                 .WithMany(bld => bld.ProjetoEstruturasOrganizacionais)
                 .HasForeignKey(bld => bld.IdSuperior)
                 .HasPrincipalKey(bld => bld.Id)
                 .HasConstraintName("FKProjetoEstruturaOrganizacionalSuperior")
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
