using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Usuario : Entity
    {
        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public bool Administrador { get; set; }

        public bool Ativo { get; set; }

        public ICollection<PerfilUsuario> PerfisUsuario { get; set; }

        public ICollection<UsuarioFuncionalidade> UsuarioFuncionalidades { get; set; }

        public ICollection<Indicador> Indicadores { get; set; }
    }
}
