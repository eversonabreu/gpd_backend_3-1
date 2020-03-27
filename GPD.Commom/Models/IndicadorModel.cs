using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class IndicadorModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O idenficador do indicador é obrigatório.")]
        [StringLength(maximumLength: IndicadorConsts.TamanhoColunaIdentificador, ErrorMessage = "O idenficador do indicador não pode conter mais que 30 caracteres.")]
        public string Identificador { get; set; }

        [Required(ErrorMessage = "O campo 'Ativo' é obrigatório.")]
        public bool Ativo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do indicador é obrigatório.")]
        [StringLength(maximumLength: IndicadorConsts.TamanhoColunaNome, ErrorMessage = "O nome do indicador não pode conter mais que 255 caracteres.")]
        public string Nome { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = "O campo 'ordem' não possui um valor válido.")]
        public short? Ordem { get; set; }

        [Required(ErrorMessage = "O campo 'Valor percentual do peso' é obrigatório.")]
        [Range(type: typeof(decimal), minimum: "0", maximum: "100", ErrorMessage = "O campo 'Valor percentual do peso' deve possuir um valor entre '0,00' e '100,00'.")]
        public decimal ValorPercentualPeso { get; set; }

        [Required(ErrorMessage = "O campo 'Valor percentual do critério' é obrigatório.")]
        [Range(type: typeof(decimal), minimum: "0", maximum: "100", ErrorMessage = "O campo 'Valor percentual do critério' deve possuir um valor entre '0,00' e '100,00'.")]
        public decimal ValorPercentualCriterio { get; set; }

        [Required(ErrorMessage = "O tipo de remuneração é obrigatório.")]
        [Range(minimum: 1, maximum: 4, ErrorMessage = "O tipo de remuneração não possui um valor válido.")]
        public int TipoRemuneracao { get; set; }

        [Required(ErrorMessage = "O tipo de cardinalidade é obrigatório.")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "O tipo de cardinalidade não possui um valor válido.")]
        public int TipoCardinalidade { get; set; }

        [Required(ErrorMessage = "O tipo de periodicidade é obrigatório.")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "O tipo de periodicidade não possui um valor válido.")]
        public int TipoPeriodicidade { get; set; }

        [Required(ErrorMessage = "O tipo de acúmulo da meta é obrigatório.")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "O tipo de acúmulo da meta não possui um valor válido.")]
        public int TipoAcumuloMeta { get; set; }

        [Required(ErrorMessage = "O tipo de acúmulo do realizado é obrigatório.")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "O tipo de acúmulo do realizado não possui um valor válido.")]
        public int TipoAcumuloRealizado { get; set; }

        [Required(ErrorMessage = "O id da unidade de medida é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id da unidade de medida é inválido.")]
        public long IdUnidadeMedida { get; set; }

        [Required(ErrorMessage = "O campo 'Corporativo' é obrigatório.")]
        public bool Corporativo { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do usuário responsável é inválido.")]
        public long? IdUsuarioResponsavel { get; set; }

        public string Formula { get; set; }

        public string Observacao { get; set; }

        public decimal? ValorMinimoAtingimento { get; set; }

        public decimal? ValorMaximoAtingimento { get; set; }

        public decimal? ValorMinimoPonderado { get; set; }

        public decimal? ValorMaximoPonderado { get; set; }

        public override void AdditionalValidations()
        {
            Identificador = Identificador.Trim().ToUpper();
        }
    }
}
