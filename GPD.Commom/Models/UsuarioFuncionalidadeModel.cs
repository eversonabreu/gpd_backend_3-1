using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class UsuarioFuncionalidadeModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O id do usuário é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do usuário não é válido.")]
        public long IdUsuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O id da funcionalidade é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id da funcionalidade não é válido.")]
        public long IdFuncionalidade { get; set; }

        [Required(ErrorMessage = "O campo 'permite inserir' é obrigatório.")]
        public bool PermiteInserir { get; set; }

        [Required(ErrorMessage = "O campo 'permite editar' é obrigatório.")]
        public bool PermiteEditar { get; set; }

        [Required(ErrorMessage = "O campo 'permite excluir' é obrigatório.")]
        public bool PermiteExcluir { get; set; }
    }
}
