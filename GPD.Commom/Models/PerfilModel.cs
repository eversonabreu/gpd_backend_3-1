using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class PerfilModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do perfil é obrigatório.")]
        [StringLength(maximumLength: PerfilConsts.TamanhoColunaNome, ErrorMessage = "O nome do perfil não pode conter mais que 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(maximumLength: PerfilConsts.TamanhoColunaDescricao, ErrorMessage = "A descrição do perfil não pode conter mais que 500 caracteres.")]
        public string Descricao { get; set; }
    }
}
