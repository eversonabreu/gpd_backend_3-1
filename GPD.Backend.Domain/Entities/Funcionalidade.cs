using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Funcionalidade : Entity
    {
        public string Nome { get; set; }

        public string Controlador { get; set; }

        public ICollection<UsuarioFuncionalidade> UsuariosFuncionalidade { get; set; }
    }
}
