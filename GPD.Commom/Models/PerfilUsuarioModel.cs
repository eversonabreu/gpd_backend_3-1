using GPD.Commom.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class PerfilUsuarioModel : Model
    {
        [Required(ErrorMessage = "O id do perfil é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do perfil é não é válido.")]
        public long IdPerfil { get; set; }

        [Required(ErrorMessage = "O id do usuário é obrigatório.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do usuário é não é válido.")]
        public long IdUsuario { get; set; }
    }
}