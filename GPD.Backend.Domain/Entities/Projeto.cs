using GPD.Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Projeto : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataTermino { get; set; }

        public ICollection<IndicadorLancamento> IndicadorLancamentos { get; set; }
    }
}
