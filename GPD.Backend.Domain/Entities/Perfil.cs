using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Perfil : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<PerfilUsuario> PerfilUsuarios { get; set; }
    }
}
