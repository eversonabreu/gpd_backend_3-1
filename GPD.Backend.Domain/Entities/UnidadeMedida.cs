using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class UnidadeMedida : Entity
    {
        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public ICollection<Indicador> Indicadores { get; set; }
    }
}
