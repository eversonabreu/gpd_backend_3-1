using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;
using System;

namespace GPD.Backend.Domain.Entities
{
    public sealed class IndicadorLancamento : Entity
    {
        public long IdProjeto { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdProjeto), TypeRepository = typeof(IProjetoRepository))]
        public Projeto Projeto { get; set; }

        public long IdIndicador { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdIndicador), TypeRepository = typeof(IIndicadorRepository))]
        public Indicador Indicador { get; set; }

        public DateTime DataLancamento { get; set; }

        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }
    }
}
