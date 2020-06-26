﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPD.Backend.Infrastructure.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200409201049_ImportacaoLancamentos")]
    partial class ImportacaoLancamentos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Auditoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime");

                    b.Property<long>("IdRegistro")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("NomeTabela")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Objeto")
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PkAuditoria");

                    b.HasIndex("NomeTabela", "IdRegistro");

                    b.ToTable("Auditoria");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Estado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoUfIbge")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.HasKey("Id")
                        .HasName("PkEstado");

                    b.HasIndex("CodigoUfIbge")
                        .IsUnique()
                        .HasName("UkEstadoCodigoUfIbge");

                    b.HasIndex("Nome")
                        .IsUnique()
                        .HasName("UkEstadoNome");

                    b.HasIndex("Sigla")
                        .IsUnique()
                        .HasName("UkEstadoSigla");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Funcionalidade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Controlador")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PkFuncionalidade");

                    b.HasIndex("Controlador")
                        .IsUnique()
                        .HasName("UkFuncionalidade");

                    b.ToTable("Funcionalidade");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Indicador", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcumulaMeta")
                        .HasColumnType("bit");

                    b.Property<bool>("AcumulaRealizado")
                        .HasColumnType("bit");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<bool>("Corporativo")
                        .HasColumnType("bit");

                    b.Property<string>("Formula")
                        .HasColumnType("text");

                    b.Property<long>("IdUnidadeMedida")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdUsuarioResponsavel")
                        .HasColumnType("bigint");

                    b.Property<string>("Identificador")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Observacao")
                        .HasColumnType("text");

                    b.Property<short?>("Ordem")
                        .HasColumnType("smallint");

                    b.Property<bool>("PossuiCardinalidade")
                        .HasColumnType("bit");

                    b.Property<int>("TipoCalculo")
                        .HasColumnType("integer");

                    b.Property<int>("TipoPeriodicidade")
                        .HasColumnType("integer");

                    b.Property<int>("TipoRemuneracao")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorPercentualCriterio")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<decimal>("ValorPercentualPeso")
                        .HasColumnType("decimal(3, 2)");

                    b.HasKey("Id")
                        .HasName("PkIndicador");

                    b.HasIndex("IdUnidadeMedida");

                    b.HasIndex("IdUsuarioResponsavel");

                    b.HasIndex("Identificador")
                        .IsUnique()
                        .HasName("UkIndicador");

                    b.ToTable("Indicador");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.IndicadorLancamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<long>("IdIndicador")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProjeto")
                        .HasColumnType("bigint");

                    b.Property<int>("Mes")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorMeta")
                        .HasColumnType("decimal(20, 2)");

                    b.Property<decimal>("ValorRealizado")
                        .HasColumnType("decimal(20, 2)");

                    b.HasKey("Id")
                        .HasName("PkIndicadorLancamento");

                    b.HasIndex("IdIndicador");

                    b.HasIndex("IdProjeto")
                        .HasName("UkIndicadorLancamentoPro");

                    b.HasIndex("IdProjeto", "IdIndicador")
                        .HasName("UkIndicadorLancamentoProIn");

                    b.HasIndex("IdProjeto", "IdIndicador", "Mes", "Ano")
                        .IsUnique()
                        .HasName("UkIndicadorLancamento");

                    b.ToTable("IndicadorLancamento");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Municipio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoMunicipioIbge")
                        .HasColumnType("integer");

                    b.Property<long>("IdEstado")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PkMunicipio");

                    b.HasIndex("CodigoMunicipioIbge")
                        .IsUnique()
                        .HasName("UkMunicipioCodigoMunicipioIbge");

                    b.HasIndex("IdEstado");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.NivelOrganizacional", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PkNivelOrganizacional");

                    b.ToTable("NivelOrganizacional");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Perfil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PkPerfil");

                    b.HasIndex("Codigo")
                        .IsUnique()
                        .HasName("UkPerfil");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.PerfilUsuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("IdPerfil")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PkPerfilUsuario");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("IdPerfil", "IdUsuario")
                        .IsUnique()
                        .HasName("UkPerfilUsuario");

                    b.ToTable("PerfilUsuario");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Projeto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id")
                        .HasName("PkProjeto");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.ProjetoEstruturaOrganizacional", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("IdIndicador")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdNivelOrganizacional")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProjeto")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdSuperior")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PkProjetoEstruturaOrganizacional");

                    b.HasIndex("IdIndicador");

                    b.HasIndex("IdNivelOrganizacional");

                    b.HasIndex("IdProjeto");

                    b.HasIndex("IdSuperior");

                    b.HasIndex("IdUsuario");

                    b.ToTable("ProjetoEstruturaOrganizacional");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.UnidadeMedida", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id")
                        .HasName("PkUnidadeMedida");

                    b.ToTable("UnidadeMedida");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Administrador")
                        .HasColumnType("bit");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ValorPesoCorporativo")
                        .HasColumnType("decimal(20,2)");

                    b.Property<decimal>("ValorPesoIndividual")
                        .HasColumnType("decimal(20,2)");

                    b.HasKey("Id")
                        .HasName("PkUsuario");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasName("UkUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.UsuarioFuncionalidade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("IdFuncionalidade")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<bool>("PermiteEditar")
                        .HasColumnType("bit");

                    b.Property<bool>("PermiteExcluir")
                        .HasColumnType("bit");

                    b.Property<bool>("PermiteInserir")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PkUsuarioFuncionalidade");

                    b.HasIndex("IdFuncionalidade");

                    b.HasIndex("IdUsuario", "IdFuncionalidade")
                        .IsUnique()
                        .HasName("UkUsuarioFuncionalidade");

                    b.ToTable("UsuarioFuncionalidade");
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Indicador", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.UnidadeMedida", "UnidadeMedida")
                        .WithMany("Indicadores")
                        .HasForeignKey("IdUnidadeMedida")
                        .HasConstraintName("FKIndicadorUnidadeMedida")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GPD.Backend.Domain.Entities.Usuario", "UsuarioResponsavel")
                        .WithMany("Indicadores")
                        .HasForeignKey("IdUsuarioResponsavel")
                        .HasConstraintName("FKIndicadorUsuarioResponsavel")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.IndicadorLancamento", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.Indicador", "Indicador")
                        .WithMany("IndicadorLancamentos")
                        .HasForeignKey("IdIndicador")
                        .HasConstraintName("FKIndicadorLancamentoIndicador")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GPD.Backend.Domain.Entities.Projeto", "Projeto")
                        .WithMany("IndicadorLancamentos")
                        .HasForeignKey("IdProjeto")
                        .HasConstraintName("FKIndicadorLancamentoProjeto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.Municipio", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.Estado", "Estado")
                        .WithMany("Municipios")
                        .HasForeignKey("IdEstado")
                        .HasConstraintName("FKMunicipioEstado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.PerfilUsuario", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.Perfil", "Perfil")
                        .WithMany("PerfilUsuarios")
                        .HasForeignKey("IdPerfil")
                        .HasConstraintName("FKPerfilUsuarioPerfil")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GPD.Backend.Domain.Entities.Usuario", "Usuario")
                        .WithMany("PerfisUsuario")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FKPerfilUsuarioUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.ProjetoEstruturaOrganizacional", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.Indicador", "Indicador")
                        .WithMany("ProjetoEstruturasOrganizacionais")
                        .HasForeignKey("IdIndicador")
                        .HasConstraintName("FKProjetoEstruturaOrganizacionalIndicador")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GPD.Backend.Domain.Entities.NivelOrganizacional", "NivelOrganizacional")
                        .WithMany("ProjetoEstruturasOrganizacionais")
                        .HasForeignKey("IdNivelOrganizacional")
                        .HasConstraintName("FKProjetoEstruturaOrganizacionalNivelOrganizacional")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GPD.Backend.Domain.Entities.Projeto", "Projeto")
                        .WithMany("ProjetoEstruturasOrganizacionais")
                        .HasForeignKey("IdProjeto")
                        .HasConstraintName("FKProjetoEstruturaOrganizacionalProjeto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GPD.Backend.Domain.Entities.ProjetoEstruturaOrganizacional", "Superior")
                        .WithMany("ProjetoEstruturasOrganizacionais")
                        .HasForeignKey("IdSuperior")
                        .HasConstraintName("FKProjetoEstruturaOrganizacionalSuperior")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GPD.Backend.Domain.Entities.Usuario", "Usuario")
                        .WithMany("ProjetoEstruturasOrganizacionais")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FKProjetoEstruturaOrganizacionalUsuario")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GPD.Backend.Domain.Entities.UsuarioFuncionalidade", b =>
                {
                    b.HasOne("GPD.Backend.Domain.Entities.Funcionalidade", "Funcionalidade")
                        .WithMany("UsuariosFuncionalidade")
                        .HasForeignKey("IdFuncionalidade")
                        .HasConstraintName("FKUsuarioFuncionalidadePerfil")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GPD.Backend.Domain.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioFuncionalidades")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FKUsuarioFuncionalidadeUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
