using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Repositories
{
    public interface IIndicadorLancamentoRepository : IBaseRepository<IndicadorLancamento>
    {
        ImpotacaoLancamentosDto GerarLancamentos(IEnumerable<ImpotacaoLancamentos> lancamentos);
        IList<RelatorioFiltroResultado> ObterLancamentosParaRelatorio(RelatorioFiltro filtro);
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

    public class RelatorioFiltro
    {
        public long? IdProjeto { get; set; }
        public long? IdIndicador { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdCargo { get; set; }
        public int MesInicial { get; set; }
        public int AnoInicial { get; set; }
        public int MesFinal { get; set; }
        public int AnoFinal { get; set; }
    }

    public class RelatorioFiltroResultado
    {
        public long IdSuperior { get; set; }
        public long IdUsuario { get; set; }
        public long IdProjeto { get; set; }
        public long IdIndicador { get; set; }
        public string NomeIndicador { get; set; }
        public string Identificador { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal ValorPercentualPeso { get; set; }
        public decimal ValorPercentualCriterio { get; set; }
        public long IdCargo { get; set; }
        public decimal ValorMeta { get; set; }
        public decimal ValorRealizado { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeCargo { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string NomeProjeto { get; set; }
    }
}
