using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Usuario : Entity
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public bool Administrador { get; set; }

        public bool Ativo { get; set; }

        public string Telefone { get; set; }

        public decimal ValorPesoIndividual { get; set; }

        public decimal ValorPesoCorporativo { get; set; }

        public string EnderecoCep { get; set; }

        public string EnderecoLogradouro { get; set; }

        public string EnderecoNumero { get; set; }

        public string EnderecoComplemento { get; set; }

        public string EnderecoBairro { get; set; }

        public long? IdMunicipio { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdMunicipio), TypeRepository = typeof(IMunicipioRepository))]
        public Municipio Municipio { get; set; }

        public ICollection<PerfilUsuario> PerfisUsuario { get; set; }

        public ICollection<UsuarioFuncionalidade> UsuarioFuncionalidades { get; set; }

        public ICollection<Indicador> Indicadores { get; set; }

        public ICollection<ProjetoEstruturaOrganizacional> ProjetoEstruturasOrganizacionais { get; set; }
    }
}
