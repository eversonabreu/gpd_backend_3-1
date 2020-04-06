using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public enum TipoProjetoEstruturaOrganizacional
    {
        Projeto = 1,
        Corporativo = 2,
        Grupo = 3,
        Departamento = 4,
        Cargo = 5,
        Usuario = 6,
        Indicador = 7
    }

    public sealed class ProjetoEstruturaOrganizacional : Entity
    {
        public long? IdSuperior { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdSuperior), TypeRepository = typeof(IProjetoEstruturaOrganizacionalRepository))]
        public ProjetoEstruturaOrganizacional Superior { get; set; }

        public long IdProjeto { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdProjeto), TypeRepository = typeof(IProjetoRepository))]
        public Projeto Projeto { get; set; }

        public string Descricao { get; set; }

        public TipoProjetoEstruturaOrganizacional Tipo { get; set; }

        public int PosicaoEstrutura { get; set; }

        public long? IdNivelOrganizacional { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdNivelOrganizacional), TypeRepository = typeof(INivelOrganizacionalRepository))]
        public NivelOrganizacional NivelOrganizacional { get; set; }

        public long? IdUsuario { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdUsuario), TypeRepository = typeof(IUsuarioRepository))]
        public Usuario Usuario { get; set; }

        public long? IdIndicador { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdIndicador), TypeRepository = typeof(IIndicadorRepository))]
        public Indicador Indicador { get; set; }

        public ICollection<ProjetoEstruturaOrganizacional> ProjetoEstruturasOrganizacionais { get; set; }
    }
}
