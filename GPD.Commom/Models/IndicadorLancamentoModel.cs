using GPD.Commom.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class IndicadorLancamentoModel : Model
    {
        [Required(ErrorMessage = "O id do projeto é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do projeto é inválido.")]
        public long IdProjeto { get; set; }

        [Required(ErrorMessage = "O id do indicador é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do indicador é inválido.")]
        public long IdIndicador { get; set; }

        [Required(ErrorMessage = "O mês é obrigatório.")]
        [Range(minimum: 1, maximum: 12, ErrorMessage = "Mês inválido.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(minimum: 2015, maximum: 2050, ErrorMessage = "Ano inválido.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Valor da meta é obrigatório.")]
        public decimal ValorMeta { get; set; }

        [Required(ErrorMessage = "Valor do realizado é obrigatório.")]
        public decimal ValorRealizado { get; set; }
    }
}
