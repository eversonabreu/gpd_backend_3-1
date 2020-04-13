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

        [Required(AllowEmptyStrings = false, ErrorMessage = "O CPF do usuário é obrigatório.")]
        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaCpf, ErrorMessage = "O CPF do usuário não pode conter mais que 11 caracteres.")]
        public string Cpf { get; set; }

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

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaTelefone, ErrorMessage = "O telefone do usuário não pode conter mais que 15 caracteres.")]
        public string Telefone { get; set; }

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaCep, ErrorMessage = "O CEP do endereço não pode conter mais que 9 caracteres.")]
        public string EnderecoCep { get; set; }

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaLogradouro, ErrorMessage = "O logradouro do endereço não pode conter mais que 255 caracteres.")]
        public string EnderecoLogradouro { get; set; }

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaNumero, ErrorMessage = "O número do endereço não pode conter mais que 20 caracteres.")]
        public string EnderecoNumero { get; set; }

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaComplemento, ErrorMessage = "O complemento do endereço não pode conter mais que 255 caracteres.")]
        public string EnderecoComplemento { get; set; }

        [StringLength(maximumLength: UsuarioConsts.TamanhoColunaBairro, ErrorMessage = "O bairro do endereço não pode conter mais que 255 caracteres.")]
        public string EnderecoBairro { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "O id do município é inválido.")]
        public long? IdMunicipio { get; set; }
    }
}
