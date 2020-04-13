using GPD.Backend.Domain.Entities;
using GPD.Commom.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPD.Backend.Infrastructure.Database.Mappings
{
    public sealed class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));
            builder.HasKey(bld => bld.Id).HasName("PkUsuario");
            builder.Property(bld => bld.Id).IsRequired();
            builder.Property(bld => bld.Nome).HasMaxLength(UsuarioConsts.TamanhoColunaNome).IsRequired();
            builder.Property(bld => bld.Cpf).HasMaxLength(UsuarioConsts.TamanhoColunaCpf).IsRequired();
            builder.Property(bld => bld.Senha).HasColumnType("text").IsRequired();
            builder.Property(bld => bld.Email).HasMaxLength(UsuarioConsts.TamanhoColunaEmail).IsRequired();
            builder.Property(bld => bld.Administrador).IsRequired();
            builder.Property(bld => bld.Ativo).IsRequired();
            builder.Property(bld => bld.ValorPesoIndividual).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(bld => bld.ValorPesoCorporativo).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(bld => bld.Telefone).HasMaxLength(UsuarioConsts.TamanhoColunaTelefone);
            builder.Property(bld => bld.EnderecoCep).HasMaxLength(UsuarioConsts.TamanhoColunaCep);
            builder.Property(bld => bld.EnderecoLogradouro).HasMaxLength(UsuarioConsts.TamanhoColunaLogradouro);
            builder.Property(bld => bld.EnderecoNumero).HasMaxLength(UsuarioConsts.TamanhoColunaNumero);
            builder.Property(bld => bld.EnderecoComplemento).HasMaxLength(UsuarioConsts.TamanhoColunaComplemento);
            builder.Property(bld => bld.EnderecoBairro).HasMaxLength(UsuarioConsts.TamanhoColunaBairro);
            builder.Property(bld => bld.IdMunicipio);

            builder.HasOne(bld => bld.Municipio)
             .WithMany(bld => bld.Usuarios)
             .HasForeignKey(bld => bld.IdMunicipio)
             .HasPrincipalKey(bld => bld.Id)
             .HasConstraintName("FKUsuarioMunicipio")
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bld => bld.Cpf).HasName("UkUsuario").IsUnique();
        }
    }
}
