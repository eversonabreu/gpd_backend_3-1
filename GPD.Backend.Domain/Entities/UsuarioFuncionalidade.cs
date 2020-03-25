using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;

namespace GPD.Backend.Domain.Entities
{
    public sealed class UsuarioFuncionalidade : Entity
    {
        public long IdUsuario { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdUsuario), TypeRepository = typeof(IUsuarioRepository))]
        public Usuario Usuario { get; set; }

        public long IdFuncionalidade { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdFuncionalidade), TypeRepository = typeof(IFuncionalidadeRepository))]
        public Funcionalidade Funcionalidade { get; set; }

        public bool PermiteInserir { get; set; }

        public bool PermiteEditar { get; set; }

        public bool PermiteExcluir { get; set; }
    }
}
