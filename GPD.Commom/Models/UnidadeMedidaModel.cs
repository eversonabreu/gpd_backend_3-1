using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class UnidadeMedidaModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A descrição da unidade de medida é obrigatório.")]
        [StringLength(maximumLength: UnidadeMedidaConsts.TamanhoColunaDescricao, ErrorMessage = "A descrição da unidade de medida não pode conter mais que 255 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A sigla da unidade de medida é obrigatório.")]
        [StringLength(maximumLength: UnidadeMedidaConsts.TamanhoColunaSigla, ErrorMessage = "A sigla da unidade de medida não pode conter mais que 10 caracteres.")]
        public string Sigla { get; set; }
    }
}
