using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;

namespace GPD.Backend.Domain.Entities
{
    public sealed class PerfilUsuario : Entity
    {
        public long IdPerfil { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdPerfil), TypeRepository = typeof(IPerfilRepository))]
        public Perfil Perfil { get; set; }

        public long IdUsuario { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdUsuario), TypeRepository = typeof(IUsuarioRepository))]
        public Usuario Usuario { get; set; }
    }
}
