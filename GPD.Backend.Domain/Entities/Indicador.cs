﻿using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;
using System.Collections.Generic;
using System.ComponentModel;

namespace GPD.Backend.Domain.Entities
{
    public enum TipoRemuneracao
    {
        [Description("Não remunerado")]
        NaoRemunerado = 1,
        [Description("Anual")]
        Anual = 2,
        [Description("Mensal")]
        Mensal = 3,
        [Description("Diário")]
        Diario = 4
    }

    public enum TipoCardinalidade
    {
        [Description("Exata")]
        Exata = 1,
        [Description("Quanto maior melhor")]
        QuantoMaiorMelhor = 2,
        [Description("Quanto menor melhor")]
        QuantoMenorMelhor = 3
    }

    public enum TipoAcumulo
    {
        [Description("Não acumulável")]
        NaoAcumulavel = 1,
        [Description("Acumulável")]
        Acumulavel = 2,
        [Description("Média")]
        Media = 3
    }

    public enum TipoPeriodicidade
    {
        [Description("Diário")]
        Diario = 1,
        [Description("Mensal")]
        Mensal = 2,
        [Description("Anual")]
        Anual = 3
    }

    public enum TipoCalculo
    {
        [Description("Não calculado")]
        NaoCalculado = 1,
        [Description("Somente meta")]
        SomenteMeta = 2,
        [Description("Somente realizado")]
        SomenteRealizado = 3,
        [Description("Ambos")]
        Ambos = 4
    }

    public sealed class Indicador : Entity
    {
        public string Identificador { get; set; }

        public bool Ativo { get; set; }

        public string Nome { get; set; }

        public short? Ordem { get; set; }

        public decimal ValorPercentualPeso { get; set; }

        public decimal ValorPercentualCriterio { get; set; }

        public TipoRemuneracao TipoRemuneracao { get; set; }

        public TipoCardinalidade TipoCardinalidade { get; set; }

        public TipoPeriodicidade TipoPeriodicidade { get; set; }

        public TipoAcumulo TipoAcumuloMeta { get; set; }

        public TipoAcumulo TipoAcumuloRealizado { get; set; }

        public long IdUnidadeMedida { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdUnidadeMedida), TypeRepository = typeof(IUnidadeMedidaRepository))]
        public UnidadeMedida UnidadeMedida { get; set; }

        public bool Corporativo { get; set; }

        public long? IdUsuarioResponsavel { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdUsuarioResponsavel), TypeRepository = typeof(IUsuarioRepository))]
        public Usuario UsuarioResponsavel { get; set; }

        public string Formula { get; set; }

        public string Observacao { get; set; }

        public decimal? ValorMinimoAtingimento { get; set; }

        public decimal? ValorMaximoAtingimento { get; set; }

        public decimal? ValorMinimoPonderado { get; set; }

        public decimal? ValorMaximoPonderado { get; set; }

        public ICollection<IndicadorLancamento> IndicadorLancamentos { get; set; }
    }
}