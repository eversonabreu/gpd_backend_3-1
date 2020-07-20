using System.Collections.Generic;

namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IIndicadorLancamentosService
    {
        IndicadorLancamentosResultado ObterResultadosPorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        UsuarioIndicadorLancamentosResultado ObterResultadosParaUsuario(long idProjetoEstruturaOrganizacional, long idUsuario, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        IList<IndicadorLancamentosResultado> ObterResultadosPorProjeto(long idProjeto, int mesInicial, int anoInicial, int mesFinal, int anoFinal);

        IndicadorLancamentosEvolucaoMensal ObterResultadosPorIndicadorEvolucaoMensalSimples(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal);
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
}
