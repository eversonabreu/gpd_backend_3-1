using GPD.Backend.Domain.Repositories;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IIndicadorLancamentosService
    {
        IndicadorLancamentosResultado ObterResultadosPorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        UsuarioIndicadorLancamentosResultado ObterResultadosParaUsuario(long idProjetoEstruturaOrganizacional, long idUsuario, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        IList<IndicadorLancamentosResultado> ObterResultadosPorProjeto(long idProjeto, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        IndicadorLancamentosEvolucaoMensal ObterResultadosPorIndicadorEvolucaoMensalSimples(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        IndicadorResultadosRelatorio GerarResultadosParaRelatorio(RelatorioFiltro filtro);
    }

    public class IndicadorLancamentosResultado
    {
        public string NomeIndicador { get; set; }

        public string UnidadeMedida { get; set; }

        public string Script { get; set; }

        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }

        public decimal ValorAtingimento { get; set; }

        public decimal Peso { get; set; }
		
		public decimal Criterio { get; set; }
		
		public bool Cardinalidade { get; set; }
    }

    public struct UsuarioIndicadorLancamentosResultado
    {
        public decimal ValorPonderadoIndividual { get; set; }

        public decimal ValorPonderadoCorporativo { get; set; }

        public decimal ValorPonderadoFinal { get; set; }

        public string Script1 { get; set; }

        public string Script2 { get; set; }

        public string Script3 { get; set; }

        public IList<IndicadorLancamentosResultado> Indicadores { get; set; }
    }

    public struct IndicadorLancamentosEvolucaoMensal
    {
        public string NomeIndicador { get; set; }

        public string UnidadeMedida { get; set; }

        public IList<IndicadorLancamentosEvolucaoMensalSimples> IndicadorEvolucaoMensalSimples { get; set; }
    }

    public struct IndicadorLancamentosEvolucaoMensalSimples
    {
        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }

        public decimal ValorAtingimento { get; set; }

        public string Ocorrencia { get; set; }
    }

    public class IndicadorResultadosRelatorio
    {
        public string Periodo { get; set; }

        public IList<IndicadorResultadosRelatorioProjeto> Projetos { get; set; }
    }

    public class IndicadorResultadosRelatorioProjeto
    {
        public long IdProjeto { get; set; }

        public string NomeProjeto { get; set; }

        public IList<IndicadorResultadosRelatorioCargo> Cargos { get; set; }
    }

    public class IndicadorResultadosRelatorioCargo
    {
        public string Cargo { get; set; }

        public IList<IndicadorResultadosRelatorioUsuario> Usuarios { get; set; }
    }

    public class IndicadorResultadosRelatorioUsuario
    {
        public string Usuario { get; set; }

        public IList<IndicadorResultadosRelatorioIndicador> Indicadores { get; set; }

        public decimal PonderadoInvidual { get; set; }
        
        public decimal PonderadoCorporativo { get; set; }

        public decimal PonderadoFinal { get; set; }
    }

    public class IndicadorResultadosRelatorioIndicador
    {
        public string NomeIndicador { get; set; }

        public string Identificador { get; set; }

        public string UnidadeMedida { get; set; }

        public decimal ValorPercentualPeso { get; set; }

        public decimal ValorPercentualCriterio { get; set; }

        public IList<IndicadorResultadosRelatorioLancamento> Lancamentos { get; set; }

        public decimal ValorMetaCalculado { get; set; }

        public decimal ValorRealizadoCalculado { get; set; }

        public decimal ValorAtingimento { get; set; }
    }

    public class IndicadorResultadosRelatorioLancamento
    {
        public int Ano { get; set; }

        public string Mes { get; set; }

        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }
    }
}
