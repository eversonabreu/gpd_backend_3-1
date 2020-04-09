using GPD.Commom.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class ProjetoEstruturaOrganizacionalModel : Model
    {
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id superior é inválido.")]
        public long? IdSuperior { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o id do projeto.")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do projeto é inválido.")]
        public long IdProjeto { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o tipo.")]
        [Range(minimum: 1, 7, ErrorMessage = "O tipo é inválido.")]
        public int Tipo { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id da nível organizacional é inválido.")]
        public long? IdNivelOrganizacional { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do usuário é inválido.")]
        public long? IdUsuario { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do indicador é inválido.")]
        public long? IdIndicador { get; set; }
    }
}
