namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IEnderecoService
    {
        EnderecoCepConsulta ObterEnderecoPorCep(string cep);
    }

    public class EnderecoCepConsulta
    {
        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public int Ibge { get; set; }

        public long IdMunicipio { get; set; }

        public long IdEstado { get; set; }
    }
}
