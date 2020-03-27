using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class NivelOrganizacionalModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do nível organizacional é obrigatório.")]
        [StringLength(maximumLength: NivelOrganizacionalConsts.TamanhoColunaNome, ErrorMessage = "O nome do perfil não pode conter mais que 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo deve ser informado obrigatoriamente.")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "O valor do campo 'tipo' não é válido.")]
        public int Tipo { get; set; }

        [StringLength(maximumLength: NivelOrganizacionalConsts.TamanhoColunaDescricao, ErrorMessage = "A descrição do nível organizacional não pode conter mais que 500 caracteres.")]
        public string Descricao { get; set; }
    }
}
