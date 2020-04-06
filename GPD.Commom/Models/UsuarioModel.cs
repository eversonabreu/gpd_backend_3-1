using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class UsuarioModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaNome, ErrorMessage = "O nome do usuário não pode conter mais que 255 caracteres.")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O login do usuário é obrigatório.")]
        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaLogin, ErrorMessage = "O login do usuário não pode conter mais que 150 caracteres.")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha do usuário é obrigatório.")]
        public string Senha { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O e-mail do usuário é obrigatório.")]
        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaEmail, ErrorMessage = "O e-mail do usuário não pode conter mais que 150 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo administrador é obrigatório.")]
        public bool Administrador { get; set; }

        [Required(ErrorMessage = "O campo ativo é obrigatório.")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo 'Valor peso individual' é obrigatório.")]
        public decimal ValorPesoIndividual { get; set; }

        [Required(ErrorMessage = "O campo 'Valor peso corporativo' é obrigatório.")]
        public decimal ValorPesoCorporativo { get; set; }

        public override void AdditionalValidations()
        {
            Login = Login?.Trim().ToUpper();
        }
    }
}
