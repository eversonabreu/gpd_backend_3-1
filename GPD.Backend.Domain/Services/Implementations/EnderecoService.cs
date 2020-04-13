using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System;
using System.Net.Http;
using System.Text.Json;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IMunicipioRepository municipioRepository;

        public EnderecoService(IMunicipioRepository municipioRepository) => this.municipioRepository = municipioRepository;

        public EnderecoCepConsulta ObterEnderecoPorCep(string cep)
        {
            try
            {
                const string urlViaCep = "https://viacep.com.br/ws/{0}/json/";
                using var http = new HttpClient();
                string json = http.GetStringAsync(string.Format(urlViaCep, cep)).Result;
                var endereco = JsonSerializer.Deserialize<EnderecoCepConsulta>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var municipio = municipioRepository.FirstOrDefault(item => item.CodigoMunicipioIbge == endereco.Ibge, loadDependencies: false);
                endereco.IdMunicipio = municipio.Id;
                endereco.IdEstado = municipio.IdEstado;
                return endereco;
            }
            catch (Exception exc)
            {
                Console.Out.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
