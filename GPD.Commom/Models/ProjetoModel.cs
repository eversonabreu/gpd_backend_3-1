using GPD.Commom.Consts;
using GPD.Commom.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPD.Commom.Models
{
    public sealed class ProjetoModel : Model
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do projeto é obrigatório.")]
        [StringLength(maximumLength: ProjetoConsts.TamanhoColunaNome, ErrorMessage = "O nome do projeto não pode conter mais do que 150 caracteres.")]
        public string Nome { get; set; }

        [StringLength(maximumLength: ProjetoConsts.TamanhoColunaDescricao, ErrorMessage = "A descrição do projeto não pode conter mais do que 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'Ativo' é obrigatório.")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatório.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de término é obrigatório.")]
        public DateTime DataTermino { get; set; }
    }
}
