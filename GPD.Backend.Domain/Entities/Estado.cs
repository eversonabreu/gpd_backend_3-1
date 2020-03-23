using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Estado : Entity
    {
        public int CodigoUfIbge { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
    }
}
