using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Repositories
{
    public interface IIndicadorLancamentoRepository : IBaseRepository<IndicadorLancamento>
    {
        ImpotacaoLancamentosDto GerarLancamentos(IEnumerable<ImpotacaoLancamentos> lancamentos);
    }

    public struct ImpotacaoLancamentos
    {
        public long IdProjeto { get; set; }
        public string Identificador { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal ValorMeta { get; set; }
        public decimal ValorRealizado { get; set; }
    }

    public struct ImpotacaoLancamentosDto
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }
    }
}
